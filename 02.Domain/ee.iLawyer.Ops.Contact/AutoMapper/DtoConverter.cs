using AutoMapper;
using System.Collections.Generic;
using System.Linq;


namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public static class DtoConverter
    {
        public static DTO.Area Convert(Db.Entities.Foundation.Area source)
        {
            return Mapper.Map<DTO.Area>(source);
        }
        public static DTO.ProjectCategory ConvertToProjectCategory(Db.Entities.Foundation.Picklist source)
        {
            return Mapper.Map<DTO.ProjectCategory>(source);
        }
        public static DTO.ProjectCause ConvertToProjectCause(Db.Entities.Foundation.Picklist source)
        {
            return Mapper.Map<DTO.ProjectCause>(source);
        }
        public static DTO.PropertyPicker ConvertToPropertyPicker(Db.Entities.Foundation.Picklist source)
        {
            return Mapper.Map<Contact.DTO.PropertyPicker>(source);
        }
        public static DTO.Category Convert(DTO.PropertyPicker source)
        {
            return Mapper.Map<DTO.Category>(source);
        }

        public static DTO.SystemManagement.User Convert(Db.Entities.RBAC.SysUser source)
        {
            var target = Mapper.Map<DTO.SystemManagement.User>(source);

            var resources = (from c in source.Permissions select (c.Code))
                .Union(from c in source.Roles.SelectMany(x => x.Permissions) select (c.Code))
                .Except(from c in source.Restrictions select (c.Code));
            target.Resources = resources.Distinct().ToList();

            return target;
        }
        public static DTO.SystemManagement.Role Convert(Db.Entities.RBAC.SysRole source)
        {
            return Mapper.Map<DTO.SystemManagement.Role>(source);
        }



        public static DTO.Court Convert(Db.Entities.Court source)
        {
            return Mapper.Map<DTO.Court>(source);
        }
        public static DTO.Judge Convert(Db.Entities.Judge source)
        {
            return Mapper.Map<DTO.Judge>(source);
        }
        public static DTO.Client Convert(Db.Entities.Client source)
        {
            return Mapper.Map<DTO.Client>(source);
        }
        public static DTO.Project Convert(Db.Entities.Project source)
        {
            if (source == null)
            {
                return null;
            }

            var destination = Mapper.Map<DTO.Project>(source);

            destination.Account = Mapper.Map<DTO.ProjectAccount>(source?.Account ?? null);
            //---------------------
            var involvedClients = new List<DTO.Client>();
            if (source.InvolvedClients != null && source.InvolvedClients.Any())
            {
                foreach (var item in source.InvolvedClients)
                {
                    if (item.Client != null)
                    {
                        involvedClients.Add(Convert(item.Client));
                    }
                }
            }
            destination.InvolvedClients = involvedClients;
            //---------------------
            var todoList = new List<DTO.ProjectTodoItem>();
            if (source.TodoList != null && source.TodoList.Any())
            {
                foreach (var item in source.TodoList)
                {
                    todoList.Add(Convert(item));
                }
            }
            destination.TodoList = todoList;
            //--------------------
            var progresses = new List<DTO.ProjectProgress>();
            if (source.Progresses != null && source.Progresses.Any())
            {
                foreach (var item in source.Progresses)
                {
                    progresses.Add(Convert(item));
                }
            }
            destination.Progresses = progresses;

            return destination;
        }

        public static DTO.ProjectAccount Convert(Db.Entities.ProjectAccount source)
        {
            return Mapper.Map<DTO.ProjectAccount>(source);
        }
        public static DTO.ProjectProgress Convert(Db.Entities.ProjectProgress source)
        {
            return Mapper.Map<DTO.ProjectProgress>(source);
        }
        public static DTO.ProjectTodoItem Convert(Db.Entities.ProjectTodoItem source)
        {

            return Mapper.Map<DTO.ProjectTodoItem>(source);
        }












        public static Db.Entities.ProjectAccount Convert(DTO.ProjectAccount source)
        {

            return Mapper.Map<Db.Entities.ProjectAccount>(source);
        }

        public static Db.Entities.ProjectProgress Convert(DTO.ProjectProgress source, Db.Entities.Project project)
        {
            if (source == null)
            {
                return null;
            }
            var target = Mapper.Map<Db.Entities.ProjectProgress>(source);
            target.InProject = project;
            return target;
        }
        public static Db.Entities.ProjectTodoItem Convert(DTO.ProjectTodoItem source, Db.Entities.Project project)
        {
            if (source == null)
            {
                return null;
            }
            var target = Mapper.Map<Db.Entities.ProjectTodoItem>(source);
            target.InProject = project;
            return target;
        }

        public static IList<DTO.Category> Convert(IList<DTO.PropertyPicker> source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new List<DTO.Category>();
            foreach (var item in source)
            {
                var category = Mapper.Map<DTO.Category>(item);
                target.Add(category);
            }
            return target;
        }

    }
}
