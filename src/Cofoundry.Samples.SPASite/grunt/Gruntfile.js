module.exports = function(grunt) {

    "use strict";
    
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        watch: {
            sass:{
                files: ['../sass/**/*.scss'],
                tasks: ['compass:dev']
            },
            js: {
                files: ['../Content/js/**/*.js'],
                tasks: ['concat']
            },
            options: {
                spawn: false,
            }
        },
        compass: {  
            config:'../config.rb',
            options: {
                basePath:'../',
                sassDir:'sass',
                cssDir:'Content/css'
            },
            dev: {
                options:{
                    outputStyle:'expanded',
                }
            },
            prod: {
                options:{
                    force:true,
                    outputStyle:'compressed',
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
                    '../Content/js/third_party/backbone/jquery-2.1.4.min.js',
                    '../Content/js/third_party/backbone/underscore-min.js',
                    '../Content/js/third_party/backbone/backbone-min.js',
                    '../Content/js/third_party/bootstrap.min.js',
                    '../Content/js/core/app.js',
                    '../Content/js/core/models/*.js',
                    '../Content/js/core/collections/*.js',
                    '../Content/js/core/views/*.js',
                    '../Content/js/core/views/components/*.js',
                    '../Content/js/core/views/pages/*.js',
                    '../Content/js/core/init.js',
                ],
                // the location of the resulting JS file
                dest: '../Content/js/compiled/<%= pkg.name %>.js'
            }
        },
        uglify: {
            options: {
                // the banner is inserted at the top of the output
                banner: '/*! <%= pkg.name %> <%= grunt.template.today("dd-mm-yyyy") %> */\n'
            },
            compiled: {
                files: {
                    '../Content/js/compiled/<%= pkg.name %>.min.js': ['<%= concat.compiled.dest %>']
                }
            }
        }
    });
    
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-compass');
    grunt.loadNpmTasks('grunt-contrib-imagemin');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-concat');
    
    grunt.registerTask('default', ['compass:prod', 'concat']);
};
