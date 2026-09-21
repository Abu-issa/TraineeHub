namespace TraineeHub.Infrastructure.Email
{
    public static class EmailTemplates
    {
        public static string SubmissionReceived(string traineeName, string topic)
        {
            return $@"
            <div style='font-family:Arial;padding:20px;background:#f4f4f4'>
                <div style='max-width:600px;margin:auto;background:white;padding:20px;border-radius:10px'>

                    <h2 style='color:#2c3e50'>🎉 Submission Received</h2>

                    <p>Hi <b>{traineeName}</b>,</p>

                    <p>Your submission for:</p>

                    <h3 style='color:#3498db'>{topic}</h3>

                    <p style='color:green'><b>has been successfully received ✅</b></p>

                    <hr/>

                    <p style='font-size:12px;color:gray'>
                        TraineeHub System - Automated Notification
                    </p>

                </div>
            </div>";
        }
    }
}