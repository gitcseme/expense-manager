<template>
    <GChart
        v-if="isTreeDataReady"
        type="PieChart"
        :options="googlePieChart.options"
        :data="googlePieChart.data"
    />
</template>

<script>
import { GChart } from 'vue-google-charts';
import JsonTreeBuilder from "@scripts/Utilities/JsonTreeBuilder";

export default {
    props: [ 'pieData' ],
    components: {
        GChart
    },
    data() {
        return {
            googlePieChart: {
                data: [],
                options: {
                    //width: 1100,
                    height: 400,
                },
            },
            isTreeDataReady: false,
            categories: this.pieData
        };
    },
    mounted() {
        this.prepareData();
        console.log('in-pie-view: ', this.categories);
    },
    methods: {
        prepareData() {
            let categoryTree = JsonTreeBuilder.buildTree(this.categories);
            this.googlePieChart.data = [['Expense Category', 'Expense Amount']];
            this.calculateExpenses(categoryTree, this.googlePieChart.data);
            this.isTreeDataReady = true;
        },
        calculateExpenses(tree, data) {
            let allChildNodeExpense = 0;
            tree.forEach(node => {
                let selfExpense = 0;
                node.expenses.forEach(expense => { // Self expense
                    selfExpense += expense.amount;
                });

                let nodeExpense = selfExpense + this.calculateExpenses(node.children, data);
                
                if(node.parentId == null) 
                    data.push([node.label, nodeExpense]);
                
                allChildNodeExpense += nodeExpense;
            });

            return allChildNodeExpense;
        }
    }
}
</script>