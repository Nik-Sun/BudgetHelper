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
        public const string DeleteAlertTitle = "Внимание";
        public const string DeletedTitle = "Успешно изтрито";




        public const string PopUpClose = "Затвори";
        public const string ActionTypeDelete = "Изтрии типа";
        public const string ActionTypeCheck = "Виж всички разходи от този тип";
        public const string DeleteConfirmMessage = "Сигурни ли сте че искате да изтриете типа {0}?Изтривайки го ще изтриете всички разходи асоциирани с него.";
        public const string DeletedMessage = "Успешно изтрихте {0}";
    }
}
