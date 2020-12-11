using System;
using ProjectManagement.Api.Models;

namespace ProjectManagement.Api.Entities
{
    public class ProjectMessage : IMessage<Project>, IEquatable<ProjectMessage>
    {
        private readonly Project _project;

        public ProjectMessage(Project project)
        {
            _project = project;
        }

        public Project GetContent()
        {
            return _project;
        }

        public bool Equals(ProjectMessage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_project, other._project);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProjectMessage) obj);
        }

        public override int GetHashCode()
        {
            return (_project != null ? _project.GetHashCode() : 0);
        }
    }
}