module.exports = function (grunt) {

    "use strict";

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        watch: {
            sass: {
                files: ['../sass/**/*.scss'],
                tasks: ['compass:dev']
            },
            js: {
                files: ['../wwwroot/js/**/*.js'],
                tasks: ['concat']
            },
            options: {
                spawn: false,
            }
        },
        compass: {
            config: '../config.rb',
            options: {
                basePath: '../',
                sassDir: 'sass',
                cssDir: 'wwwroot/css'
            },
            dev: {
                options: {
                    outputStyle: 'expanded',
                }
            },
            prod: {
                options: {
                    force: true,
                    outputStyle: 'compressed',
                }
            }
        },
        // JS concatenation and minification is handled by the .NET build process
        concat: {
            options: {
                // define a string to put between each file in the concatenated output
                separator: ';'
            },
            compiled: {
                // the files to concatenate
                src: [
                    // to ensure everything works properly, we need to excercise some control over concat order
                    '../wwwroot/js/third_party/backbone/jquery-2.1.4.min.js',
                    '../wwwroot/js/third_party/backbone/underscore-min.js',
                    '../wwwroot/js/third_party/backbone/backbone-min.js',
                    '../wwwroot/js/third_party/bootstrap.min.js',
                    '../wwwroot/js/core/helper.js',
                    '../wwwroot/js/core/app.js',
                    '../wwwroot/js/core/models/*.js',
                    '../wwwroot/js/core/collections/*.js',
                    '../wwwroot/js/core/views/itemViews/*.js',
                    '../wwwroot/js/core/views/collectionViews/*.js',
                    '../wwwroot/js/core/views/components/*.js',
                    '../wwwroot/js/core/views/pages/*.js',
                    '../wwwroot/js/core/views/*.js',
                    '../wwwroot/js/core/init.js',
                ],
                // the location of the resulting JS file
                dest: '../wwwroot/js/compiled/<%= pkg.name %>.js'
            }
        },
        uglify: {
            options: {
                // the banner is inserted at the top of the output
                banner: '/*! <%= pkg.name %> <%= grunt.template.today("dd-mm-yyyy") %> */\n'
            },
            compiled: {
                files: {
                    '../wwwroot/js/compiled/<%= pkg.name %>.min.js': ['<%= concat.compiled.dest %>']
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-compass');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-concat');

    grunt.registerTask('default', ['compass:prod', 'concat']);
};
