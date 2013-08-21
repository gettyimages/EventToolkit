require 'rubygems'
require 'rake'

WINDOWS = ENV['OS'] == 'Windows_NT'
ROOT = File.expand_path(File.dirname(__FILE__))
SPECS_PATH = File.join(ROOT, 'specs/bin/Debug/specs.dll')

task :default => 'specs'

task :compile do
  WINDOWS ? compile_with_msbuild : compile_with_xbuild
end

task :specs => :compile do
  WINDOWS ? specs_with_dotnet : specs_with_mono
end

def compile_with_xbuild
  raise "Failed to compile with xbuild" unless \
    system '/usr/bin/xbuild EventToolkit.sln /verbosity:minimal'
end

def compile_with_msbuild
  raise "Failed to compile with msbuild" unless \
    system 'C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe EventToolkit.sln /verbosity:minimal'
end

def specs_with_mono
  system "mono --runtime=v4.0 ./packages/NUnit.Runners.2.6.0.12051/tools/nunit-console.exe #{SPECS_PATH} -nologo -xml=.specs.xml"
end

def specs_with_dotnet
  system ".\\packages\\NUnit.Runners.2.6.0.12051\\tools\\nunit-console.exe #{SPECS_PATH} -nologo -xml=.specs.xml"
end
