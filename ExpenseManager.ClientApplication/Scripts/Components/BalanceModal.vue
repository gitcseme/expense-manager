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
          <div class="form-group">
            <label>Amount</label>
            <input v-validate="'required'" data-vv-name='amount' type="text" class="form-control" v-model="vmIncome.amount">
            <span class="text-danger">{{ errors.first('amount') }}</span>
          </div>
          <div>
            <label>Date</label>
          </div>
          <div class="form-group">
            <DatePicker
              v-validate="'required'"
              data-vv-name="date"
              v-model="vmIncome.time"
              valueType="format"
            ></DatePicker>
            <span class="text-danger">{{ errors.first('date') }}</span>
          </div>
          <div class="form-group">
            <label>Comment</label>
            <input type="text" class="form-control" v-model="vmIncome.comment">
          </div>
        </div>
      </div>
      <slot></slot>
      <button v-if="mode === 'create'" class="btn btn-success" @click="saveIncome">Save</button>
      <button v-else class="btn btn-success" @click="updateIncome">Update</button>
    </dialog>
  </div>
</template>

<script>
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

export default {
  components: {
    DatePicker
  },
  props: ['income', 'mode'],
	data() {
		return {
			vmIncome: this.income,
      time: '2021-10-25'
		};
	},
	methods: {
    saveIncome() {
      this.$validator.validateAll().then(valid => {
        if (valid) {
          this.$emit('onAddIncome', this.vmIncome);
          this.$emit('close');
        }
      });
    },
    updateIncome() {
      this.$validator.validateAll().then(valid => {
        if (valid) {
          this.$emit('onUpdateIncome', this.vmIncome);
			    this.$emit('close');
        }
      });
    },
  }
}
</script>

<style>
/* // .backdrop {
//   position: fixed;
//   top: 0;
//   left: 0;
//   width: 100%;
//   height: 100vh;
//   z-index: 10;
//   background-color: rgba(0, 0, 0, 0.75);
// }

// dialog {
//   position: fixed;
//   top: 15vh;
//   width: 33rem;
//   left: calc(50% - 15rem);
//   margin: 0;
//   box-shadow: 0 2px 8px rgba(0, 0, 0, 0.26);
//   border-radius: 3px;
//   padding: 1rem;
//   background-color: cadetblue;
//   z-index: 100;
//   border: none;
// } */
</style>