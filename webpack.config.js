const path = require('path')
const webpack = require('webpack')
const ExtractTextPlugin = require('extract-text-webpack-plugin')
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin
const merge = require('webpack-merge')
const nodeExternals = require('webpack-node-externals')

module.exports = (env) => {
  const isDevBuild = !(env && env.prod)

  // Configuration in common to both client-side and server-side bundles
  const sharedConfig = () => ({
    target: 'node',
    externals: [nodeExternals()],
    stats: { modules: false },
    resolve: { extensions: ['.js', '.jsx', '.ts', '.tsx'] },
    output: {
      filename: '[name].js',
      publicPath: 'dist/' // Webpack dev middleware, if enabled, handles requests for this URL prefix
    },
    module: {
      loaders: [
        {
          test: /\.jsx?$/,
          enforce: 'pre',
          include: /Client/,
          exclude: path.resolve(__dirname, 'node_modules'),
          use: 'eslint-loader'
        },
        {
          test: /\.jsx?$/,
          include: /Client/,
          exclude: path.resolve(__dirname, 'node_modules'),
          use: 'react-hot-loader',
          query: {
            presets: ['react', 'es2015', 'stage-1'],
            compact: false
          }
        }
      ],
      rules: [
        { test: /\.tsx?$/, include: /Client/, use: 'awesome-typescript-loader?silent=true' },
        { test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' }
      ]
    },
    plugins: [new CheckerPlugin()]
  })

  // Configuration for client-side bundle suitable for running in browsers
  const clientBundleOutputDir = './wwwroot/dist'
  const clientBundleConfig = merge(sharedConfig(), {
    entry: { 'main-client': './Client/index.js' },
    module: {
      rules: [
        { test: /\.css$/, use: ExtractTextPlugin.extract({ use: isDevBuild ? 'css-loader' : 'css-loader?minimize' }) }
      ]
    },
    output: { path: path.join(__dirname, clientBundleOutputDir) },
    plugins: [
      new ExtractTextPlugin('site.css'),
      new webpack.DllReferencePlugin({
        context: __dirname,
        manifest: require('./wwwroot/dist/vendor-manifest.json')
      })
    ].concat(isDevBuild ? [
      // Plugins that apply in development builds only
      new webpack.SourceMapDevToolPlugin({
        filename: '[file].map', // Remove this line if you prefer inline source maps
        moduleFilenameTemplate: path.relative(clientBundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
      })
    ] : [
      // Plugins that apply in production builds only
      new webpack.optimize.UglifyJsPlugin()
    ])
  })

  // Configuration for server-side (prerendering) bundle suitable for running in Node
  const serverBundleConfig = merge(sharedConfig(), {
    resolve: { mainFields: ['main'] },
    entry: { 'main-server': './Client/dist/vendor.js' },
    plugins: [
      new webpack.DllReferencePlugin({
        context: __dirname,
        manifest: require('./Client/dist/vendor-manifest.json'),
        sourceType: 'commonjs2',
        name: './vendor'
      })
    ],
    output: {
      libraryTarget: 'commonjs',
      path: path.join(__dirname, './Client/dist')
    },
    devtool: 'inline-source-map'
  })

  return [clientBundleConfig, serverBundleConfig]
}
