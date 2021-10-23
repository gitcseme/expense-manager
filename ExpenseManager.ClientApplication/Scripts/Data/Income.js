export default class Income {
    constructor(id=0, amount=0, time=null, comment='', companyId=null) {
        this.id = id;
        this.amount = amount;
        this.time = time;
        this.comment = comment;
        this.companyId = companyId;
    }
}