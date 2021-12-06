import DashboardLayout from "@scripts/layout/dashboard/DashboardLayout.vue";
// GeneralViews
import NotFound from "@scripts/pages/NotFoundPage.vue";

// Admin pages
import Dashboard from "@scripts/pages/Dashboard.vue";
import UserProfile from "@scripts/pages/UserProfile.vue";
import Notifications from "@scripts/pages/Notifications.vue";
import Icons from "@scripts/pages/Icons.vue";
import Maps from "@scripts/pages/Maps.vue";
import Typography from "@scripts/pages/Typography.vue";
import TableList from "@scripts/pages/TableList.vue";
import CategoriesLayout from "@scripts/pages/Categories/CategoriesLayout.vue";
import Category from "@scripts/pages/Categories/Category.vue";
import Expenses from "@scripts/pages/Expenses/Expenses.vue";
import BulkExpense from "@scripts/pages/Expenses/BulkExpense.vue";
import Report from "@scripts/pages/Reports/Report.vue";
import Balances from "@scripts/pages/Balances/Balances.vue";
import Login from "@scripts/components/Login.vue";
import Register from "@scripts/components/Register.vue";
import InviteUsers from "@scripts/components/InviteUsers.vue";

const routes = [
  {
    path: "/",
    component: DashboardLayout,
    redirect: "/dashboard",
    children: [
      {
        path: "dashboard",
        name: "dashboard",
        component: Dashboard
      },
      {
        path: "categories",
        name: "categories",
        component: CategoriesLayout,
        redirect: "categories/category/0",
        children: [
          {
            path: 'category/:id',
            name: 'category',
            component: Category
          }
        ]
      },
      {
        path: "expenses",
        name: "expenses",
        component: Expenses
      },
      {
        path: 'report',
        name: 'report',
        component: Report
      },
      {
        path: 'balance',
        name: 'balance',
        component: Balances
      },
      { 
        path: 'invite', 
        name: 'invite', 
        component: InviteUsers 
      },
      {
        path: "stats",
        name: "stats",
        component: UserProfile
      },
      {
        path: "notifications",
        name: "notifications",
        component: Notifications
      },
      {
        path: "icons",
        name: "icons",
        component: Icons
      },
      {
        path: "maps",
        name: "maps",
        component: Maps
      },
      {
        path: "typography",
        name: "typography",
        component: Typography
      },
      {
        path: "table-list",
        name: "table-list",
        component: TableList
      },
    ]
  },
  { path: "/bulk-expense", name: "bulk-expense", component: BulkExpense },
  { path: '/login', name: 'login', component: Login },
  { path: '/register', name: 'register', component: Register },
  { path: "*", component: NotFound }
];

/**
 * Asynchronously load view (Webpack Lazy loading compatible)
 * The specified component must be inside the Views folder
 * @param  {string} name  the filename (basename) of the view to load.
function view(name) {
   var res= require('../components/Dashboard/Views/' + name + '.vue');
   return res;
};**/

export default routes;
