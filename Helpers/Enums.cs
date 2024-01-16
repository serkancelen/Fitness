using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Helpers
{
    public class Enums
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ExerciseType
        {
            [Display(Name = "Kardiyo")]
            Cardio,
            [Display(Name = "Kuvvet")]
            Strength,
            [Display(Name = "Esneklik")]
            Flexibility,
        }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Gender
        {
            [Display(Name = "Erkek")]
            Male,
            [Display(Name = "Kadın")]
            Female,
            [Display(Name = "Diğer")]
            Other
        }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum FitnessLevel
        {
            [Display(Name = "Başlangıç")]
            Beginner,
            [Display(Name = "Orta")]
            Intermediate,
            [Display(Name = "İleri")]
            Advanced,
        }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum WorkoutStatus
        {
            [Display(Name = "Tamamlandı")]
            Completed,
            [Display(Name = "Devam Ediyor")]
            InProgress,
            [Display(Name = "İptal Edildi")]
            Canceled,
        }
        [JsonConverter(typeof (JsonStringEnumConverter))]
        public enum NutritionType
        {
            [Display(Name = "Kahvaltı")]
            Breakfast,
            [Display(Name = "Öğle Yemeği")]
            Lunch,
            [Display(Name = "Akşam Yemeği")]
            Dinner,
            [Display(Name = "Ara Öğün")]
            Snack
        }

    }
}