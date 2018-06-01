using System;
using System.IO;

namespace EncapApp
{
    public class MessageStore
    {
        private readonly StoreCache cache; 
        private readonly StoreLogger log;
        private readonly FileStore fileStore;

        // First refactor for SRP.
        // 1. Change types to objects in WorkingDirectory, Read, and GetFileinfo.
        public MessageStore(DirectoryInfo workingDirectory)
        {
            if (workingDirectory == null)
                throw new ArgumentNullException("workingDirectory");
            if (!workingDirectory.Exists)
                throw new ArgumentException("Boo", "workingDirectory");

            this.WorkingDirectory = workingDirectory;
            this.cache = new StoreCache();
            this.log = new StoreLogger();
            this.fileStore = new FileStore();
        }

        public DirectoryInfo WorkingDirectory { get; private set; }
        public void Save(int id, string message) {
            this.log.Saving(id);
            var file = this.GetFileInfo(id);
            this.fileStore.WriteAllText(file.FullName, message);
            this.cache.AddOrUpdate(id, message);
            this.log.Saved(id);
        }
        public Maybe<string> Read(int id)
        {
            this.log.Reading(id);
            var file = this.GetFileInfo(id);
            if (!file.Exists)
            {
                this.log.DidNotFind(id);
                return new Maybe<string>();
            }

            var message = this.cache.GetOrAdd(id, _ => this.fileStore.ReadAllText(file.FullName));
            this.log.Returning(id);
            return new Maybe<string>(message);
        }

        public FileInfo GetFileInfo(int id) {
            return this.fileStore.GetFileInfo(id, this.WorkingDirectory.FullName);
        }


        // Original starting class
        //public FileStore(string WorkingDirectory)
        //{
        //    this.WorkingDirectory = WorkingDirectory;
        //}
        //public string WorkingDirectory { get; }
        //public void Save(int id, string message) { }
        //public Maybe<string> Read(int id)
        //{
        //    var path = this.GetFileName(id);
        //    if (!File.Exists(path))
        //        return new Maybe<string>();
        //    var message = File.ReadAllText(path);
        //    return new Maybe<string>(message);
        //}
        //public string GetFileName(int id) { }

    }
}
