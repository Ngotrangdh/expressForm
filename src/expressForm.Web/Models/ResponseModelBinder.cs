using expressForm.Core.Models.Forms;
using expressForm.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expressForm.Web.Models
{
    public class ResponseModelBinder : IModelBinder
    {
        private readonly IFormRepository _formRepository;

        public ResponseModelBinder(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            HttpRequest request = bindingContext.HttpContext.Request;
            string formId = request.RouteValues["formId"].ToString();
            var form = await _formRepository.FindAsync(int.Parse(formId));

            var responseViewModel = new ResponseViewModel
            {
                FormTitle = form.Title,
                FormDescription = form.Description,
                Answers = GetAnswers(request, form)
            };

            bindingContext.Result = ModelBindingResult.Success(responseViewModel);
        }

        private List<AnswerViewModel> GetAnswers(HttpRequest request, Form form)
        {
            var answers = new List<AnswerViewModel>();
            var answersDict = GetAnswersQuestionIdDict(request);

            foreach (var questionId in answersDict.Keys)
            {
                var question = form.Questions.SingleOrDefault(question => question.Id == questionId);

                if (question == null)
                {
                    continue;
                }

                var answer = new AnswerViewModel
                {
                    Text = answersDict[questionId],
                    //Question = question.ToViewModel()
                    Question = new QuestionViewModel
                    {
                        Id = question.Id,
                        Text = question.Text,
                        Type = question.Type.ToViewModel(),
                        Options = JsonConvert.DeserializeObject<List<string>>(question.Options),
                        IsRequired = question.IsRequired
                    }
                };

                answers.Add(answer);
            }

            return answers;
        }

        private static Dictionary<int, string> GetAnswersQuestionIdDict(HttpRequest request)
        {
            var answerDict = new Dictionary<int, string>();
            int i = 0;
            int index = 0;
            int questionId = -1;
            string answer = null;
            while (i < request.Form.Keys.Count)
            {
                var key = request.Form.Keys.ElementAt(i);
                int currentIndex;
                try
                {
                    currentIndex = int.Parse(key.Substring(8, 1));
                }
                catch
                {
                    i++;

                    if (key == "__RequestVerificationToken")
                    {
                        break;
                    }
                    continue;
                }
                var value = request.Form[key];

                if (key.EndsWith("Text"))
                {
                    answer = value;
                }
                else if (key.EndsWith("Question.Id"))
                {
                    questionId = int.Parse(value);
                }

                if (currentIndex == index)
                {
                    if (answerDict.ContainsKey(questionId))
                    {
                        if (answerDict[questionId] == null)
                        {
                        answerDict[questionId] = answer;
                        }
                        else
                        {
                            answerDict[questionId] += answer;
                        }

                        index = currentIndex;
                        i++;
                        continue;
                    }
                }

                if (!answerDict.ContainsKey(questionId))
                {
                    answerDict.Add(questionId, answer);
                }

                index = currentIndex;
                i++;
            }
            return answerDict;
        }
    }
}
