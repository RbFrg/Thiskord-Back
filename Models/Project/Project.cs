using System.Text.Json;
namespace Thiskord_Back.Models.Project
{
    public class Project
    {
        private string _id;
        private string _name;
        private string _description;

        public Project(string id, string name, string description)
        {
            this._name = name;
            this._description = description;
            this._id = id;
        }
    }

    public class ProjectRequest
    {
        public string project_name { get; set; }
        public string project_desc { get; set; }
    }

}
