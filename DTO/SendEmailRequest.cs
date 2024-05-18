namespace OllamaProject.DTO
{
    public record SendEmailRequest(
            string Subject,
            string Body,
            string to
            );

}
