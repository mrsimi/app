using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace app.Pages;

public class IndexModel : PageModel
{
    private const string QuestionsSessionKey = "Questions";
    private const string QuestionCount = "QuestionCount";
    List<string> triviaQuestions = new List<string>
        {
            "What is your biggest passion in life?",
            "If you could have dinner with any historical figure, who would it be and why?",
            "What is your favorite childhood memory?",
            "If you could travel anywhere in the world, where would you go and why?",
            "What is a book or movie that has had a significant impact on you?",
            "What is the most challenging experience you've had, and how did you grow from it?",
            "What are your thoughts on social media and its impact on society?",
            "If you could change one thing about the world, what would it be?",
            "What is one skill or hobby you've always wanted to learn?",
            "Tell me about a person who has greatly influenced your life.",
            "What is your favorite way to unwind and relax?",
            "If you could have any superpower, what would it be and why?",
            "What is your favorite quote or motto that you live by?",
            "Describe a goal or dream that you're currently pursuing.",
            "What do you value most in a friendship or relationship?",
            "What is one thing you wish more people knew about you?",
            "If you could have a conversation with your future self, what advice would you ask for?",
            "What is your favorite way to give back to your community or help others?",
            "Tell me about a moment or experience that made you feel proud of yourself.",
            "What are your thoughts on personal growth and self-improvement?",
            "If you could meet any fictional character, who would it be and why?",
            "What is your favorite form of creative expression (e.g., art, music, writing)?",
            "Tell me about a time when you stepped out of your comfort zone.",
            "What is one thing you appreciate most about your upbringing or family?",
            "What are your thoughts on the meaning of life or the pursuit of happiness?",
            "What is one thing you've always wanted to learn or try?",
            "Describe a place that holds special meaning to you and why.",
            "What is your favorite type of music, and why does it resonate with you?",
            "If you could invite three people, dead or alive, to a dinner party, who would you choose and why?",
            "Tell me about a skill or talent you possess that not many people know about.",
            "What is one value or principle that you hold dear in your life?",
            "If you could live in any era in history, which one would you choose and why?",
            "Describe a memorable trip or vacation you've been on and what made it so special.",
            "What is one cause or social issue that you feel strongly about?",
            "Tell me about a person who has been a positive influence in your life and how they've impacted you.",
            "What is your favorite way to spend a day off or a weekend?",
            "If you could have any job in the world, what would it be and why?",
            "What is a lesson or piece of advice that you've learned and would like to share?",
            "Describe a time when you overcame a challenge or obstacle and how it shaped you.",
            "What is your favorite form of exercise or physical activity?",
            "If you could have a conversation with any fictional character, who would it be and why?",
            "Tell me about a goal or dream that you've accomplished and how it made you feel.",
            "What is one thing you appreciate about your current stage in life?",
            "If you could master any skill instantly, what would it be and why?",
            "What is your favorite way to practice self-care or relaxation?",
            "Describe a funny or embarrassing moment from your life that still makes you laugh.",
            "What is one quality or characteristic that you admire in others?",
            "If you could visit any time period in the future, when would you go and what would you hope to see?",
            "Tell me about a hobby or interest you have that brings you joy.",
            "What is one thing you would like to change or improve about yourself?",
            "If you had unlimited resources, what kind of charitable work or philanthropy would you engage in?"
        };


    private IHttpContextAccessor Accessor;

    public IndexModel(IHttpContextAccessor _accessor)
    {
        this.Accessor = _accessor;
    }

    public string Question { get; set; }
    public void OnGet()
    {
        HttpContext.Session.SetInt32(QuestionCount, 2);
        HttpContext.Session.SetQuestions(QuestionsSessionKey, JsonSerializer.Serialize(triviaQuestions));
        Question = PickRandomQuestion();
    }
    public void OnGetOnNext()
    {
        Question = PickRandomQuestion();
    }

    public void OnPostNext()
    {
        Question = PickRandomQuestion();
    }


    public string PickRandomQuestion()
    {
        var questions = HttpContext.Session.GetQuestion(QuestionsSessionKey);
        int questionCount = HttpContext.Session.GetInt32(QuestionCount).GetValueOrDefault();
        if (questions.Count == 0 || questionCount == 0)
        {
            return "No more questions remaining.";
        }

        Random random = new Random();
        int randomIndex = random.Next(0, questions.Count);
        string randomQuestion = questions[randomIndex];
        questions.RemoveAt(randomIndex);

        questionCount -=1;
        HttpContext.Session.SetInt32(QuestionCount, questionCount);
        HttpContext.Session.SetQuestions(QuestionsSessionKey, JsonSerializer.Serialize(questions));
        return randomQuestion;
    }
}
