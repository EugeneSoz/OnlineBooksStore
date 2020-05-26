const path = require('path');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const webpack = require('webpack');

module.exports = {
    mode: 'production',
    optimization: {
        usedExports: true
    },
    entry: "./configs/vendors.js",
    devtool: false,
    plugins: [
        //new CleanWebpackPlugin(['dist/js/*.*'],
        //    {
        //        root: __dirname + '/../wwwroot',
        //        verbose: true
        //    }),
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
        filename: 'vendors.bundle.js',
        path: path.resolve(__dirname, '../wwwroot/dist/js')
    },
    performance: {
        maxEntrypointSize: 900000,
        maxAssetSize: 900000
    }
};