export default class Expense {
    id = 0; 
    categoryId = null; 
    title = ''; 
    amount = ''; 
    time = ''; 
    description = ''; 
    author = ''; 
    paymentMode = '';
    paymentReference = ''; 
    expenseReferenceId = '';
    companyId = null;
    constructor(value) {
        Object.assign(this, value);
    }
}