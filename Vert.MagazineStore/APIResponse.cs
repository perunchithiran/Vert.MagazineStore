﻿using System.Collections.Generic;

namespace Vert.MagazineStore
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }

    public class Subscriber
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<int> MagazineIds { get; set; }
    }

    public class SubscriberResponse : APIResponse
    {
        public List<Subscriber> Data { get; set; }
    }

    public class CategoryResponse : APIResponse
    {
        public List<string> Data { get; set; }
    }

    public class Magazine
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }

    public class MagazineResponse : APIResponse
    {
        public List<Magazine> Data { get; set; }
    }

    public class Answer
    {
        public string TotalTime { get; set; }
        public bool AnswerCorrect { get; set; }
        public List<string> ShouldBe { get; set; }
    }

    public class AnswerResponse : APIResponse
    {
        public Answer Data { get; set; }
    }

    public class AnswerRequest
    {
        public List<string> subscribers { get; set; }
    }
}