// config-overrides.js

const {
    override,
    fixBabelImports,
} = require("customize-cra");


module.exports = override(
    fixBabelImports("babel-plugin-import", {
        libraryDirectory: 'es',
        style: true
    }),
);