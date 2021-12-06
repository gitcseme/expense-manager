<template>
  <div class="modal-wrapper">
    <div class="backdrop" @click="$emit('close')"></div>
    <dialog open>
      <div class="row">
        <div class="col-12">
          <slot name="heading"></slot>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <form>
            <div class="form-label">
              <label>Date</label>
            </div>
            <div class="form-group">
              <DatePicker
                v-validate="'required'"
                data-vv-name="date"
                data-vv-as="date"
                v-model="vmExpense.time"
                valueType="format"
              ></DatePicker>
              <span class="text-danger">{{ errors.first('date') }}</span>
            </div>
            <div class="form-group">
              <label>Title</label>
              <input v-validate="'required'" data-vv-name='title' data-vv-as='title' type="text" class="form-control" v-model="vmExpense.title">
              <span class="text-danger">{{ errors.first('title') }}</span>
            </div>
            <div class="form-group">
              <label>Category</label>
              <Treeselect v-validate="'required'" data-vv-name='category' data-vv-as='category' v-model="vmExpense.categoryId" :options="options"></Treeselect>
              <span class="text-danger">{{ errors.first('category') }}</span>
            </div>
            <div class="form-group">
              <label>Amount</label>
              <input 
                v-validate="'required|numeric'" 
                data-vv-name='amount' 
                data-vv-as='amount' 
                placeholder="0.0"
                type="text" 
                class="form-control" 
                v-model="vmExpense.amount"
              >
              <span class="text-danger">{{ errors.first('amount') }}</span>
            </div>
            <div class="form-group">
              <label>Pay through</label>
              <select v-model="vmExpense.paymentMode" class="form-control">
                <option v-for="payMode in paymentOptions" :key="payMode" :value="payMode">{{ payMode }}</option>
              </select>
            </div>
            <div class="form-group">
              <label>Payment Reference</label>
              <input 
                :disabled="vmExpense.paymentMode == paymentOptions[0]" 
                type="text" 
                v-validate="'required'" 
                data-vv-name='Payment Reference' 
                class="form-control" 
                v-model="vmExpense.paymentReference"
              >
              <span class="text-danger">{{ errors.first('Payment Reference') }}</span>
            </div>
            <div class="form-group">
              <label>Desctiption</label>
              <input type="text" class="form-control" v-model="vmExpense.description">
            </div>
            <div class="form-group">
              <label>Expense Reference</label>
              <input type="text" v-validate="'required'" data-vv-name='Expense Reference Id' class="form-control" v-model="vmExpense.expenseReferenceId">
              <span class="text-danger">{{ errors.first('Expense Reference') }}</span>
            </div>
            <div class="action-buttons">
              <slot></slot>
              <button v-if="mode == 'create'" class="btn btn-success" @click.prevent="saveExpense">Save</button>
              <button v-else class="btn btn-success" @click.prevent="updateExpense">Update</button>
            </div>
          </form>
        </div>
      </div>
    </dialog>
  </div>
</template>

<script>
import Treeselect from '@riophae/vue-treeselect';
import '@riophae/vue-treeselect/dist/vue-treeselect.css';
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';
import MetadataService from '@scripts/Services/MetadataService';

export default {
  components: {
    Treeselect,
    DatePicker
  },
  props: ['options', 'expense', 'mode'],
  emits: ['close'],
  data() {
    return {
      vmExpense: this.expense,
      vmMode: this.mode,
      paymentOptions: MetadataService.getPaymentOptions(),
    };
  },
  mounted() {
    if (!!this.options && this.options[0].id == 0) {
      this.options.splice(0, 1);
    }
    this.vmExpense.paymentMode = this.vmExpense.paymentMode == '' ? this.paymentOptions[0] : this.vmExpense.paymentMode;
  },
  methods: {
    saveExpense() {
      this.$validator.validateAll().then(valid => {
        if (valid) {
          this.$emit('onCreateExpense', this.vmExpense);
          this.$emit('close');
        }
      });
    },
    updateExpense() {
      this.$validator.validateAll().then(valid => {
        if (valid) {
          this.$emit('onUpdateExpense', this.vmExpense);
			    this.$emit('close');
        }
      });
    },
  }
};
</script>
