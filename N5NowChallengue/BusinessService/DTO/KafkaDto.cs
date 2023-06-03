using System;

namespace N5NowChallengue.BusinessService.DTO
{
    public class KafkaDto
    {
        public Guid Id { get; set; }
        public string NameOperation { get; set; }

        public KafkaDto()
        {
            Id = Guid.NewGuid();
        }
    }
}