<template>
    <div class="container-fluid">
      
        <div class="row">
            <!-- <div class="col-1">
                <div class="table-responsive">
                    <div class="table table-striped table-bordered">
                        <thead>
                            <tr><th scope="col">Serial</th></tr>
                        </thead>
                        <tbody>
                            <tr v-for="(item, index) in expenses" :key="index">
                                <td>{{index+1}}</td>
                            </tr>
                        </tbody>
                    </div>
                </div>
            </div> -->
            <div class="col-12">
                <div class="scroll">
                    <table id="table-bulk-expense-entry" class="table table-striped table-bordered" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th class="frezy" scope="col">Serial</th>
                                <th scope="col"> <span style="color: red;">*</span> Expense Date</th>
                                <th scope="col"> <span style="color: red;">*</span> Title</th>
                                <th scope="col"> <span style="color: red;">*</span> Category</th>
                                <th scope="col"> <span style="color: red;">*</span> Amount</th>
                                <th scope="col"> <span style="color: red;">*</span> Pay through</th>
                                <th scope="col">Payment Reference</th>
                                <th scope="col">Expense Reference</th>
                                <th scope="col">Description</th>
                                <th scope="col">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(expense, index) in expenses" :key="index" style="border: 2px solid #eee">
                                <td>
                                    {{index+1}}
                                </td>

                                <td>
                                    <DatePicker
                                        v-validate="'required'"
                                        data-vv-name="date"
                                        data-vv-as="date"
                                        v-model="expense.time"
                                        valueType="format"
                                    ></DatePicker>
                                </td>
                                <td>
                                    <div class="data-expense">
                                        <input v-validate="'required'" data-vv-name='title' data-vv-as='title' type="text" class="form-control" v-model="expense.title">
                                    </div>
                                </td>
                                <td>
                                    <div class="data-expense">
                                        <Treeselect v-validate="'required'" data-vv-name='category' data-vv-as='category' v-model="expense.categoryId" :options="vmCategoryTree"></Treeselect>
                                    </div>
                                </td>
                                <td>
                                    <div class="data-expense">
                                        <input 
                                            v-validate="'required|numeric'" 
                                            data-vv-name='amount' 
                                            data-vv-as='amount' 
                                            placeholder="0.0"
                                            class="form-control" 
                                            type="text"
                                            v-model="expense.amount"
                                        >
                                    </div>
                                </td>
                                <td>
                                    <div class="data-expense">
                                        <select v-model="expense.paymentMode" class="form-control">
                                            <option v-for="payMode in paymentOptions" :key="payMode" :value="payMode">{{ payMode }}</option>
                                        </select>
                                    </div>
                                </td>
                                <td>
                                    <div class="data-expense">
                                        <input 
                                            type="text"   
                                            class="form-control" 
                                            v-model="expense.paymentReference"
                                            :disabled="expense.paymentMode == paymentOptions[0]"
                                        >
                                    </div>
                                </td>
                                <td>
                                    <div class="data-expense">
                                        <input type="text" v-validate="'required'" data-vv-name='Expense Reference Id' class="form-control" v-model="expense.expenseReferenceId">
                                    </div>
                                </td>
                                <td>
                                    <div class="data-expense">
                                        <textarea class="form-control" v-model="expense.description"></textarea>
                                    </div>
                                </td>
                                <td>
                                    <button @click="onDeleteExpense(index)" class="btn btn-danger">
                                        <i class="ti-trash"></i> Delete
                                    </button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="form-errors">
                        <ul>
                            <li class="text-danger" v-for="(error, index) in errors.all()" :key="index">{{ error }}</li>
                        </ul>
                    </div>
                    <!-- <div class="add-new-row">
                        <button @click="addExpense" class="btn btn-primary"><i class="ti-plus"></i> Add Expense</button>
                        <button @click="onBulkInsert" class="btn btn-outline-warning"> <i class="ti-save"></i> Save All</button>
                    </div> -->
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col">
                     <div class="add-new-row">
                        <button @click="addExpense" class="btn btn-primary"><i class="ti-plus"></i> Add Expense</button>
                        <button @click="onBulkInsert" class="btn btn-outline-success"> <i class="ti-save"></i> Save All</button>
                    </div>
            </div>
        </div>
    </div>
</template>

<script>
import Treeselect from '@riophae/vue-treeselect';
import '@riophae/vue-treeselect/dist/vue-treeselect.css';
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';
import CategoryService from "@scripts/Services/CategoryService";
import JsonTreeBuilder from "@scripts/Utilities/JsonTreeBuilder";
import ExpenseModel from '@scripts/data/Expense';
import MetadataService from '@scripts/Services/MetadataService';
import ApplicationContextService from "@scripts/Services/ApplicationContextService";
import ExpenseService from "@scripts/Services/ExpenseService";

export default {
    components: {
        Treeselect,
        DatePicker
    },
    data() {
        return {
            expenses: [new ExpenseModel()],
            vmCategoryTree: [],
            paymentOptions: MetadataService.getPaymentOptions(),
            companyId: null,
        }
    },
    mounted() {
        this.companyId = ApplicationContextService.getContext().company.id;
        this.loadCategoryTree();
    },
    methods: {
        loadCategoryTree() {
            CategoryService.getAllCategories(this.companyId)
            .then(data => {
                this.vmCategoryTree = JsonTreeBuilder.buildTree(data);
            });
        },
        addExpense() {
            this.expenses.push(new ExpenseModel());
        },
        onDeleteExpense(index) {
            this.expenses.splice(index, 1);
        },
        async onBulkInsert() {
            if (await this.$validator.validateAll()) {
                
                console.log(this.expenses);

                ExpenseService.bulkInsert(this.expenses).then(response => {
                    this.$router.push('/expenses');
                })
                .catch(error => {
                    console.dir('error: ', error);
                });
            }
        }
    }
}
</script>
