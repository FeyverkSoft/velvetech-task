// config-overrides.js

const {
    override,
    fixBabelImports,
    addBabelPlugin,
} = require("customize-cra");


module.exports = override(
    fixBabelImports("babel-plugin-import", {
        libraryDirectory: 'es',
        style: true
    }),
    addBabelPlugin([
        "@babel/plugin-transform-typescript",
        { allowNamespaces: true }
    ]),
);