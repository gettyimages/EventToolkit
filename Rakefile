require 'rubygems'
require 'rake'

ROOT = File.expand_path(File.dirname(__FILE__))
MSPEC_PATH = File.join(ROOT, 'packages/Machine.Specifications.0.5.12/tools/mspec.exe')

task :default => 'specs'

task :compile do
  ok = system("/usr/bin/xbuild #{ROOT}/*.sln /v:normal")
  raise "Failed to compile" unless ok
end

task :specs => :compile do
  system "mono --runtime=v4.0 #{MSPEC_PATH} -x ignore #{File.join(ROOT, 'specs/bin/Debug/specs.dll')}"
end
