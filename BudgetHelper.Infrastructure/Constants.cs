using Xamarin.Essentials;

namespace BudgetHelper.Core
{
    public static class DatabaseConstants
    {
       
        public const string DatabaseFilename = "BudgetHelper.db3";
        
        public static string DatabasePath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),DatabaseFilename);
    }
    public static class MessagesConstants
    {
        public const string AddNewTypeCustomName = " {Добавете нов тип разход} ";
        public const string TypePickerActionSheetTitle = "Избери Действие";
        public const string WarningPopUpTitle = "Внимание";
        public const string DeletedTitle = "Успешно изтрито";




        public const string PopUpClose = "Затвори";
        public const string PopUpConfirm = "Разбрах";
        public const string ActionTypeDelete = "Изтрии типа";
        public const string ActionTypeCheck = "Виж всички разходи от този тип";
        public const string DeleteTypeConfirmMessage = "Сигурни ли сте че искате да изтриете типа {0}?Изтривайки го ще изтриете всички разходи асоциирани с него.";
        public const string DeletedMessage = "Успешно изтрихте {0}";
        public const string InvalidExpenseValueMessage = "'{0}' не е валидна стойност за разход.";
        public const string InvalidTypeNameMessage = "Името на типа не може да е празно.";
        public const string InvalidTypeNameLengthMessage = "Името на типа трябва да е между 3 и 30 символа.";
        public const string InvalidCategoryMessage = "Няма избрана категория за разхода.";
        public const string DeleteExpenseConfirmMessage = "Продължавайки ще изтриете разхода {0}.";
    }
}
