using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public static class DtoConverter
    {
        public static DTO.Area Convert(Db.Entity.Area source)
        {
            return Mapper.Map<DTO.Area>(source);
        }
        public static DTO.ProjectCategory Convert(Db.Entity.ProjectCategory source)
        {
            return Mapper.Map<DTO.ProjectCategory>(source);
        }
        public static DTO.ProjectCause Convert(Db.Entity.ProjectCause source)
        {
            return Mapper.Map<DTO.ProjectCause>(source);
        }
        public static DTO.PropertyItemCategory Convert(Db.Entity.PropertyItemCategory source)
        {
            return Mapper.Map<Contact.DTO.PropertyItemCategory>(source);
        }
        public static DTO.Category Convert(DTO.PropertyItemCategory source)
        {
            return Mapper.Map<DTO.Category>(source);
        }

        public static DTO.Court Convert(Db.Entity.Court source)
        {
            return Mapper.Map<DTO.Court>(source);
        }
        public static DTO.Judge Convert(Db.Entity.Judge source)
        {
            return Mapper.Map<DTO.Judge>(source);
        }
        public static DTO.Client Convert(Db.Entity.Client source)
        {
            return Mapper.Map<DTO.Client>(source);
        }
        public static DTO.Project Convert(Db.Entity.Project source)
        {
            var destination = Mapper.Map<DTO.Project>(source);
            destination.Account = Mapper.Map<DTO.ProjectAccount>(source.Account);
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

        public static DTO.ProjectAccount Convert(Db.Entity.ProjectAccount source)
        {
            return Mapper.Map<DTO.ProjectAccount>(source);
        }
        public static DTO.ProjectProgress Convert(Db.Entity.ProjectProgress source)
        {
            return Mapper.Map<DTO.ProjectProgress>(source);
        }
        public static DTO.ProjectTodoItem Convert(Db.Entity.ProjectTodoItem source)
        {
            return Mapper.Map<DTO.ProjectTodoItem>(source);
        }












        public static Db.Entity.ProjectAccount Convert(DTO.ProjectAccount source)
        {
            return Mapper.Map<Db.Entity.ProjectAccount>(source);
        }

        public static Db.Entity.ProjectProgress Convert(DTO.ProjectProgress source, Db.Entity.Project project)
        {
            var target = Mapper.Map<Db.Entity.ProjectProgress>(source);
            target.InProject = project;
            return target;
        }
        public static Db.Entity.ProjectTodoItem Convert(DTO.ProjectTodoItem source, Db.Entity.Project project)
        {
            var target = Mapper.Map<Db.Entity.ProjectTodoItem>(source);
            target.InProject = project;
            return target;
        }

    }
}
