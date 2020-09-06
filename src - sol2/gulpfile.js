/// <binding AfterBuild='default' />
var gulp = require('gulp');
var del = require('del');

function clean() {
  return del([
    'SajidIrfan.Code.Template/Content/**/*'
  ]);
}

function move() {
  return gulp.src([
    'SajidIrfan.Code/**/*',
    '!SajidIrfan.Code/{bin,bin/**/*,obj,obj/**/*}',
    'SajidIrfan.Code/*.cs',
    'SajidIrfan.Code/*.csproj'
  ])
    .pipe(gulp.dest('SajidIrfan.Code.Template/Content'));
}

gulp.task('clean', gulp.series(clean));
gulp.task('default', gulp.series(clean, move));