const path = require('path');
const webpack = require('webpack');

module.exports = {
    mode: "development",
    optimization: {
        usedExports: true
    },
    entry: {
        publishers: "./src/bootstrap/main.ts"
    },
    devtool: "inline-source-map",
    devServer: {
        contentBase: "./src"
    },
    plugins: [
        new webpack.ProvidePlugin({
            $: 'jquery',
            jQuery: 'jquery'
        })
    ],
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/
            }
        ]
    },
    resolve: {
        extensions: ['.tsx', '.ts', '.js']
    },
    output: {
        filename: '[name].bundle.js',
        path: path.resolve(__dirname, '../wwwroot/dist/js')
    },
    performance: {
        maxEntrypointSize: 900000,
        maxAssetSize: 900000
    }
};