<template>
  <div>
    <!--Stats cards-->
    <div class="row">
      <div class="col-md-6" v-for="stat in statCharts" :key="stat.title">
        <stats-card>
          <div class="icon-big text-center" :class="`icon-${stat.type}`" slot="header">
            <i :class="stat.icon"></i>
          </div>
          <div class="numbers" slot="content">
            <p>{{stat.title}}</p>
            {{stat.value}}
          </div>
          <div class="stats" slot="footer">
            <i :class="stat.footerIcon"></i> {{stat.footerText}}
          </div>
        </stats-card>
      </div>
    </div>
    <!--Charts-->
    <div class="row">
      <div class="col-md-6 col-12">
        <div class="pie-wrapper card">
          <PieChartView
            v-if="isCategoriesLoded"
            :pieData="categories"
          ></PieChartView>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { StatsCard } from "@scripts/components/index";
import ApplicationContextService from "@scripts/Services/ApplicationContextService";
import IncomeService from "@scripts/Services/IncomeService";
import CategoryService from "@scripts/Services/CategoryService";
import PieChartView from "@scripts/components/PieChartView.vue";


export default {
  components: {
    StatsCard,
    PieChartView
  },
  data() {
    return {
      statCharts: [
        {
          type: "success",
          icon: "ti-wallet",
          title: "Balance",
          value: 0,
          footerText: "This month",
          footerIcon: "ti-calendar"
        }
      ],
      companyId: null,
      isCategoriesLoded: false,
      categories: []
    };
  },
  mounted() {
    this.companyId = ApplicationContextService.getContext().company.id;
    this.loadCategories();
    this.getTotalBalance();
  },
  methods: {
    getTotalBalance() {
      IncomeService.getBalance(this.companyId)
      .then(response => {
        this.statCharts[0].value = response;
      });
    },
    loadCategories() {
      CategoryService.getAllCategories(this.companyId)
      .then(response => {
        this.categories = response;
        this.isCategoriesLoded = true;
      });
    },
  }
};
</script>
<style>
</style>
