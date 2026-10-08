namespace lab1.DTOs
{
    public class CreateTestDto
    {
        public string Title { get; set; } = string.Empty;
        public int TopicId { get; set; }
        public List<CreateQuestionDto> Questions { get; set; } = new();
    }

    public class CreateQuestionDto
    {
        public string QuestionText { get; set; } = string.Empty;
        public string? QuestionType { get; set; }
        public string AnswersJson { get; set; } = string.Empty; 
        public int? Points { get; set; }
    }

    public class SubmitTestDto
    {
        public int UserId { get; set; }
        public List<UserAnswerDto> Answers { get; set; } = new();
    }

    public class UserAnswerDto
    {
        public int QuestionId { get; set; }
        public string AnswerText { get; set; } = string.Empty;
    }
}
