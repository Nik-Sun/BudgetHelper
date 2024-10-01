namespace BudgetHelper.Core.Entities
{
    public class ExpenseType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<Expense> Expenses { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
