export default class Expense {
    // constructor(id=0, categoryId=null, title='', amount=0, time='', description='', author='', paymentMode='', paymentReference='', expenseReferenceId='', companyId=null) {
    //     this.id = id;
    //     this.categoryId = categoryId;
    //     this.title = title;
    //     this.amount = amount;
    //     this.time = time;
    //     this.description = description;
    //     this.author = author;
    //     this.paymentMode = paymentMode;
    //     this.paymentReference = paymentReference;
    //     this.expenseReferenceId = expenseReferenceId;
    //     this.companyId = companyId;
    // }

    id = 0; 
    categoryId = null; 
    title = ''; 
    amount = 0; 
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