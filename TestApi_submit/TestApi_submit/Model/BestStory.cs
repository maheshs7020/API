namespace TestApi_submit.Model
{
        // BestStory.cs
        public class BestStory
    {
        public string Title { get; set; } = null;
        public string Uri { get; set; } = null;
        public string PostedBy { get; set; } = null;
        public Int64? Time { get; set; } = 0;
            public Int32? Score { get; set; } = 0;
            public Int32? CommentCount { get; set; }= 0;
        }

    
}
