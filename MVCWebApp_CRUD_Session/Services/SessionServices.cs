using Microsoft.AspNetCore.Http;

using MVCWebApp_CRUD_session.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVCWebApp_CRUD.Services
{
    public static class SessionServices
    {
        private const string CourseKeyPrefix = "Course_";

        public static void AddToCart(ISession session, Course course)
        {
            if (course == null)
            {
                System.Diagnostics.Debug.WriteLine("AddToCart: Course is null");
                return;
            }
            System.Diagnostics.Debug.WriteLine($"AddToCart: Adding course ID {course.Id}, Title: {course.Title}, Subject is {(course.Subject == null ? "null" : $"not null, Name: {course.Subject.Name}")}");
            try
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                };
                session.SetString($"{CourseKeyPrefix}{course.Id}", JsonSerializer.Serialize(course, options));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddToCart: Error serializing course ID {course.Id}: {ex.Message}");
            }
        }

        public static List<Course> GetCart(ISession session)
        {
            var cart = new List<Course>();
            if (session == null)
            {
                System.Diagnostics.Debug.WriteLine("GetCart: Session is null");
                return cart;
            }

            System.Diagnostics.Debug.WriteLine($"GetCart: Session keys: {string.Join(", ", session.Keys)}");

            foreach (var key in session.Keys)
            {
                if (key.StartsWith(CourseKeyPrefix))
                {
                    var courseJson = session.GetString(key);
                    if (string.IsNullOrEmpty(courseJson))
                    {
                        System.Diagnostics.Debug.WriteLine($"GetCart: Empty JSON for key {key}");
                        continue;
                    }
                    try
                    {
                        var options = new JsonSerializerOptions
                        {
                            ReferenceHandler = ReferenceHandler.IgnoreCycles
                        };
                        var course = JsonSerializer.Deserialize<Course>(courseJson, options);
                        if (course != null)
                        {
                            System.Diagnostics.Debug.WriteLine($"GetCart: Added course ID {course.Id}, Title: {course.Title}, Subject is {(course.Subject == null ? "null" : $"not null, Name: {course.Subject.Name}")}");
                            cart.Add(course);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"GetCart: Deserialized course is null for key {key}");
                        }
                    }
                    catch (JsonException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"GetCart: JSON deserialization error for key {key}: {ex.Message}");
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine($"GetCart: Returning {cart.Count} courses");
            return cart;
        }

        public static void RemoveFromCart(ISession session, int courseId)
        {
            System.Diagnostics.Debug.WriteLine($"RemoveFromCart: Removing course ID {courseId}");

            session.Remove($"{CourseKeyPrefix}{courseId}");
        }

        public static void ClearCart(ISession session)
        {
            System.Diagnostics.Debug.WriteLine("ClearCart: Clearing all cart keys");
            var keysToRemove = session.Keys.Where(k => k.StartsWith(CourseKeyPrefix)).ToList();
            foreach (var key in keysToRemove)
            {
                session.Remove(key);
            }
        }
    }
}