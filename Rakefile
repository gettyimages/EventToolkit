require 'rubygems'
require 'rake'

ROOT = File.expand_path(File.dirname(__FILE__))
WINDOWS = ENV['OS'] == 'Windows_NT'
MSPEC_PATH = File.join(ROOT, 'packages/Machine.Specifications.0.5.12/tools/mspec.exe')

task :default => 'specs'

task :compile do
  WINDOWS ? compile_msbuild : compile_xbuild
end

def compile_xbuild
  raise "Failed to compile with xbuild" unless \
    system '/usr/bin/xbuild DomainToolkit.sln /verbosity:minimal'
end

def compile_msbuild
  raise "Failed to compile with msbuild" unless \
    system 'C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe DomainToolkit.sln /verbosity:minimal'
end

task :specs => :compile do
  system "mono --runtime=v4.0 #{MSPEC_PATH} -x ignore #{File.join(ROOT, 'specs/bin/Debug/specs.dll')}"
end
