<template>
  <div class="expense-wrapper">
    <vue-confirm-dialog></vue-confirm-dialog>
    <ExpenseModal 
      @close="hideDialog" 
      @onCreateExpense="onCreateExpense"
      @onUpdateExpense="onUpdateExpense"
      :options="vmCategoryTree"
      :expense="vmExpense"
      :mode="vmMode"
      v-if="dialogIsVisible">
      <h3 slot="heading">Create Expense</h3>
      <button class="btn btn-warning mr-1" @click="hideDialog">Close</button>
    </ExpenseModal>
    <div class="row">
      <div class="col-12">
        <button class="btn btn-outline-primary" @click="showDialog('create')">Create Expense</button>
      </div>
    </div>

    <div class="row mt-3">
      <div class="col-md-3">
        <div class="data-wrapper card p-3" :style="{ backgroundColor: 'coral' }">
          <h4>Total Expense</h4>
          <h5>{{ vmTotalExpense }}</h5>
        </div>
      </div>
    </div>

		<div class="row">
			<div class="col-6">
				<h3>Expense history</h3>
			</div>
			<div class="col-6">
        <Treeselect class="float-right" :style="{ width: '50%'}" v-model="vmCategoryId" :options="vmCategoryTree"></Treeselect>
			</div>
		</div>

    <div class="row">
      <div class="col-12">
        <table class="table table-striped">
          <thead>
            <tr>
              <th scope="col" class="font-weight-bold">Id</th>
              <th scope="col" class="font-weight-bold">Title</th>
              <th scope="col" class="font-weight-bold">Category</th>
              <th scope="col" class="font-weight-bold">Amount</th>
              <th scope="col" class="font-weight-bold">Author</th>
              <th scope="col" class="font-weight-bold">Date</th>
              <th scope="col" class="font-weight-bold">Payment Mode</th>
              <th scope="col" class="font-weight-bold">Payment Reference</th>
              <th scope="col" class="font-weight-bold">Expense Reference</th>
              <th scope="col" class="font-weight-bold">Description</th>
              <th scope="col" class="font-weight-bold">Action</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="expense in vmExpenses" :key="expense.id">
              <th scope="row">{{ expense.uniqueCode }}</th>
              <td>{{ expense.title }}</td>
              <td>{{ expense.categoryLabel }}</td>
              <td>{{ expense.amount }}</td>
              <td>{{ expense.author }}</td>
              <td>{{ expense.time }}</td>
              <td>{{ expense.paymentMode }}</td>
              <td>{{ expense.paymentReference }}</td>
              <td>{{ expense.expenseReferenceId }}</td>
              <td>{{ expense.description }}</td>
              <td>
                <button @click="onEditExpense(expense.id)" class="btn btn-warning btn-sm mr-1">Edit</button>
                <button @click="onDeleteExpense(expense.id)" class="btn btn-danger btn-sm">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
import Card from "@scripts/components/Cards/Card.vue";
import ExpenseModal from "@scripts/components/ExpenseModal.vue";
import Expense from '@scripts/data/Expense';
import JsonTreeBuilder from "@scripts/Utilities/JsonTreeBuilder";
import Utility from "@scripts/Utilities/CommonUtilityFunction";
import Treeselect from '@riophae/vue-treeselect';
import '@riophae/vue-treeselect/dist/vue-treeselect.css';
import ApplicationContextService from "@scripts/Services/ApplicationContextService";
import ExpenseService from "@scripts/Services/ExpenseService";
import CategoryService from "@scripts/Services/CategoryService";

export default {
  components: {
    Card,
    ExpenseModal,
    Treeselect
  },
  data() {
    return {
      vmMode: 'create',
      vmExpense: new Expense(),
      vmExpenses: [],
      searchText: '',
      dialogIsVisible: false,
      vmCategoryTree: [],
      vmPagination: { pageIndex: 1, pageSize: 10 },
      vmCategoryId: 0,
      vmTotalExpense: 0,
      companyId: null
    };
  },
  mounted() {
    this.companyId = ApplicationContextService.getContext().company.id;
    this.loadExpenses();
    this.loadCategoryTree();
  },
  methods: {
    loadExpenses() {
      let categoryId = this.vmCategoryId == null ? 0 : this.vmCategoryId;
      ExpenseService.getAllExpense(this.companyId, categoryId, this.vmPagination)
        .then(response => {
          let totalExpense = 0;
          response.forEach(e => {
            totalExpense += e.amount;
            e.amount = Utility.formatMoney(e.amount);
          });
          this.vmExpenses = response;
          this.vmTotalExpense = Utility.formatMoney(totalExpense);
        });
    },
    loadCategoryTree() {
      CategoryService.getAllCategories(this.companyId)
      .then(data => {
        this.vmCategoryTree = JsonTreeBuilder.buildTree(data);
        this.vmCategoryTree.unshift({ id: 0, label: 'All', children: [] });
      });
    },
    onSearchExpenses() {
      console.log('search-text: ', this.searchText);
    },
    onCreateExpense(expense) {
      expense.companyId = this.companyId;
      ExpenseService.createExpense(expense)
        .then(() => {
          this.loadExpenses();
          Utility.showNotification(this, 'New expense is created', 'ti-announcement', 'success');
        });
    },
    onUpdateExpense(updatedExpense) {
      ExpenseService.updateExpense(updatedExpense)
        .then(() => {
          this.loadExpenses();
          Utility.showNotification(this, 'Expense is updated', 'ti-announcement', 'warning');
        });
    },
    onEditExpense(id) {
      let exp = this.vmExpenses.find(x => x.id === id);
      exp.amount = Utility.formattedToNormalMoney(exp.amount);
      this.vmExpense = new Expense(exp);
      this.showDialog('update');
    },
    onDeleteExpense(id) {
      this.$confirm({
        title: 'Are you sure?',
        message: `Press Yes to delete or No to cancel`,
        button: {
          no: 'No',
          yes: 'Yes'
        },
        callback: confirm => {
          if (confirm) {
            ExpenseService.deleteExpense(id)
              .then(() => {
                this.loadExpenses();
                Utility.showNotification(this, 'Expense is deleted', 'ti-announcement', 'danger');
              });
          }
          else console.log('no');
        }
      });
    },
    showDialog(mode) {
      this.vmMode = mode;
      this.dialogIsVisible = true;
    },
    hideDialog() {
      this.dialogIsVisible = false;
      this.vmExpense = new Expense();
    },
  },
  watch: {
    vmCategoryId() {
      this.loadExpenses();
    }
  }
}
</script>

<style>

</style>