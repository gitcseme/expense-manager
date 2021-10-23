<template>
  <div class="wrapper">
    <vue-confirm-dialog></vue-confirm-dialog>
		<BalanceModal
			@close="hideDialog"
			@onAddIncome="onAddIncome"
			@onUpdateIncome="onUpdateIncome"
			:income="vmIncome"
			:mode="vmMode"
			v-if="dialogIsVisible">
			<button class="btn btn-warning mr-1" @click="hideDialog">Close</button>
      <h3 slot="heading">Add Balance</h3>
		</BalanceModal>

    <div class="row">
      <div class="col-12">
        <button class="btn btn-outline-primary" @click="showDialog('create')">Add Balance</button>
      </div>
    </div>

    <div class="row mt-3">
      <div class="col-md-3">
        <div class="data-wrapper card p-3" :style="{ backgroundColor: 'aquamarine' }">
          <h4>Current Balance</h4>
          <h5>{{ vmTotalBalance }}</h5>
        </div>
      </div>
    </div>

		<div class="row">
			<div class="col-6">
				<h3>Balance history</h3>
			</div>
			<div class="col-6">
				<button class="btn btn-primary ml-1 float-right" @click="onSearchBalances">Search</button>
        <input placeholder="title..." type="text" v-model="vmSearchText" class="form-control float-right" :style="{ width: '50%'}">
			</div>
		</div>

		<div class="row">
			<div class="col-12">
				<table class="table table-striped">
          <thead>
            <tr>
              <th scope="col" class="font-weight-bold">Amount</th>
              <th scope="col" class="font-weight-bold">Author</th>
              <th scope="col" class="font-weight-bold">Date</th>
              <th scope="col" class="font-weight-bold">Comment</th>
              <th scope="col" class="font-weight-bold">Action</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="income in vmBalances" :key="income.id">
              <td scope="row" class="font-weight-bold">{{ income.amount }}</td>
              <td>{{ income.author }}</td>
              <td>{{ income.time }}</td>
              <td>{{ income.comment }}</td>
              <td>
                <button @click="onEditIncome(income.id)" class="btn btn-warning btn-sm">Edit</button>
                <button @click="onDeleteIncome(income.id)" class="btn btn-danger ml-1 btn-sm">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
			</div>
		</div>
	</div>
</template>

<script>
import BalanceModal from '@scripts/components/BalanceModal.vue';
import Income from '@scripts/data/Income';
import IncomeAPI from '@scripts/API/IncomeAPI';
import Card from "@scripts/components/Cards/Card.vue";
import Utility from "@scripts/Utilities/CommonUtilityFunction";
import ApplicationContextService from "@scripts/Services/ApplicationContextService";
import IncomeService from "@scripts/Services/IncomeService";

export default {
	components: {
    Card,
		BalanceModal
	},
  data() {
    return {
			vmSearchText: '',
			vmIncome: new Income(),
      vmPagination: { pageIndex: 1, pageSize: 10 },
			vmMode: 'create',
			vmBalances: [],
			dialogIsVisible: false,
      vmTotalBalance: 'BDT 0',
      companyId: null,
		};
  },
  mounted() {
    this.companyId = ApplicationContextService.getContext().company.id;
    this.loadIncomes();
  },
	methods: {
    getTotalBalance() {
      IncomeService.getBalance(this.companyId)
        .then(response => {
          this.vmTotalBalance = response;
        });
    },
    loadIncomes() {
      IncomeService.getAllIncome(this.companyId, this.vmPagination)
        .then(data => {
          this.vmBalances = data;
        });
      
      this.getTotalBalance();
    },
		onSearchBalances() {
			console.log(this.vmSearchText);
		},
		onAddIncome(income) {
      income.companyId = this.companyId;
			IncomeService.addIncome(income)
        .then(() => {
          this.loadIncomes();
          Utility.showNotification(this, 'New balance is added', 'ti-announcement', 'success');
        });
      
      this.getTotalBalance();
		},
		onUpdateIncome(income) {
			IncomeService.updateIncome(income)
        .then(response => {
          this.loadIncomes();
          Utility.showNotification(this, 'Balance is updated', 'ti-announcement', 'success');
        });
		},
		onEditIncome(id) {
			let income = this.vmBalances.find(i => i.id === id);
			this.vmIncome = new Income(income.id, Utility.formattedToNormalMoney(income.amount), income.time, income.comment);
			this.showDialog('update');
		},
		onDeleteIncome(id) {
      this.$confirm({
        title: 'Are you sure?',
        message: `Press Yes to delete or No to cancel`,
        button: {
          no: 'No',
          yes: 'Yes'
        },
        callback: confirm => {
          if (confirm) {
            IncomeService.deleteIncome(id)
            .then(response => {
              this.loadIncomes();
              Utility.showNotification(this, 'Balance is deleted', 'ti-announcement', 'danger');
            });
          }
        }
      });
		},
		showDialog(mode) {
      this.vmMode = mode;
      this.dialogIsVisible = true;
    },
		hideDialog() {
			this.dialogIsVisible = false;
			this.vmIncome = new Income();
		}
	}
}
</script>

<style>
</style>