namespace N5NowChallengue.Config
{
    public class AppSettings
    {
        public Settings Settings { get; set; }
    }
    public class Settings
    {
        public string DatabaseConnection { get; set; }
        public string ElasticSearchConnection { get; set; }
        public string KafkaConnection { get; set; }
        public string ElasticSearchDefaultIndex { get; set; }
    }
}