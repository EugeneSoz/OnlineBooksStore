const path = require('path');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
    mode: 'production',
    optimization: {
        usedExports: true
    },
    entry: './scss/styles.scss',
    devtool: false,
    plugins: [
        //new CleanWebpackPlugin(['dist/css/*.*'],
        //    {
        //        root: __dirname + '/../wwwroot',
        //        verbose: true
        //    }),
        new MiniCssExtractPlugin({
            filename: "app-bundle.css",
            chunkFilename: "[id].css"
        })
    ],
    module: {
        rules: [
            {
                test: /\.(scss)$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    'css-loader', // translates CSS into CommonJS modules {
                    {
                        loader: 'postcss-loader', // Run postcss actions
                        options: {
                            plugins: function () { // postcss plugins, can be exported to postcss.config.js
                                return [
                                    require('autoprefixer')
                                ];
                            }
                        }
                    },
                    'sass-loader' // compiles Sass to CSS
                ]
            },
            {
                test: /\.(png|svg|jpg|gif)$/,
                use: [
                    'file-loader'
                ]
            }
        ]
    },
    output: {
        path: path.resolve(__dirname, '../wwwroot/dist/css')
    },
    performance: {
        maxEntrypointSize: 900000,
        maxAssetSize: 900000
    }
};