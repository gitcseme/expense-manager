const mix = require('laravel-mix');
const webpack = require('./webpack.config.js');

mix
  .js("./Scripts/main.js", "./public/scripts")
  .sass("./Scripts/Assets/sass/Application.scss", "./public/css")
  .options({ processCssUrls: false })
  .setPublicPath("./")
  .copyDirectory("./Scripts/Assets/css", "./public/css")
  .copyDirectory("./Scripts/Assets/fonts", "./public/fonts")
  .copyDirectory("./Scripts/Assets/images", "./public/images")
  .copyDirectory("./public/images", "../ExpenseManager.Web/wwwroot/images")
  .copyDirectory("./public/fonts", "../ExpenseManager.Web/wwwroot/fonts")
  .copyDirectory("./public/css", "../ExpenseManager.Web/wwwroot/css")
  .copyDirectory("./public/scripts", "../ExpenseManager.Web/wwwroot/js")
  .sourceMaps()
  .webpackConfig(Object.assign(webpack))
  .options({ extractVueStyles: true });