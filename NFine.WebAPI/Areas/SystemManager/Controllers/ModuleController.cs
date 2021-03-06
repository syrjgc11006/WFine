﻿using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace NFine.WebAPI.Controllers
{
    public class ModuleController : ApiController
    {
        private ModuleApp moduleApp = new ModuleApp();

        public string GetListJson()
        { 
            var data = moduleApp.GetList();
            return data.ToJson();
        }

        public string GetTreeSelectJson()
        {
            var data = moduleApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (ModuleEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_FullName;
                treeModel.parentId = item.F_ParentId;
                treeList.Add(treeModel);
            }
            return treeList.ToJson();
        }

        public string GetTreeGridJson(string keyword)
        {
            var data = moduleApp.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.F_FullName.Contains(keyword));
            }
            var treeList = new List<TreeGridModel>();
            foreach (ModuleEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                treeModel.id = item.F_Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return  treeList.TreeGridJson();
        }

        public string GetFormJson(string keyValue)
        {
            var data = moduleApp.GetForm(keyValue);
            return data.ToJson() ;
        }

        public string SubmitForm(ModuleEntity moduleEntity, string keyValue)
        {
            moduleApp.SubmitForm(moduleEntity, keyValue);
            return  "操作成功。" ;
        }

        public string DeleteForm(string keyValue)
        {
            moduleApp.DeleteForm(keyValue);
            return  "删除成功。" ;
        }
    }
}
