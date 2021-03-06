﻿using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Our.Umbraco.InnerContent.Models
{
    public class DetachedPublishedContent : PublishedContentBase, IPublishedContentWithKey
    {
        private readonly Guid _key;
        private readonly string _name;
        private readonly PublishedContentType _contentType;
        private readonly IEnumerable<IPublishedProperty> _properties;
        private readonly int _sortOrder;
        private readonly bool _isPreviewing;
        private readonly IPublishedContent _containerNode;

        private readonly IPublishedContent _parentNode;
        private IEnumerable<IPublishedContent> _children;
        private int _level;

        public DetachedPublishedContent(Guid key,
            string name,
            PublishedContentType contentType,
            IEnumerable<IPublishedProperty> properties,
            IPublishedContent containerNode = null,
            IPublishedContent parentNode = null,
            int sortOrder = 0,
            int level = 0,
            bool isPreviewing = false)
        {
            _key = key;
            _name = name;
            _contentType = contentType;
            _properties = properties;
            _sortOrder = sortOrder;
            _level = level;
            _isPreviewing = isPreviewing;
            _containerNode = containerNode;
            _parentNode = parentNode;
            _children = Enumerable.Empty<IPublishedContent>();
        }

        public void SetChildren(IEnumerable<IPublishedContent> children)
        {
            _children = children;
        }

        public Guid Key
        {
            get { return _key; }
        }

        public override int Id
        {
            get { return 0; }
        }

        public override string Name
        {
            get { return _name; }
        }

        public override bool IsDraft
        {
            get { return _isPreviewing; }
        }

        public override PublishedItemType ItemType
        {
            get { return PublishedItemType.Content; }
        }

        public override PublishedContentType ContentType
        {
            get { return _contentType; }
        }

        public override string DocumentTypeAlias
        {
            get { return _contentType.Alias; }
        }

        public override int DocumentTypeId
        {
            get { return _contentType.Id; }
        }

        public override ICollection<IPublishedProperty> Properties
        {
            get { return _properties.ToArray(); }
        }

        public override IPublishedProperty GetProperty(string alias)
        {
            return _properties.FirstOrDefault(x => x.PropertyTypeAlias.InvariantEquals(alias));
        }

        public override IPublishedProperty GetProperty(string alias, bool recurse)
        {
            var prop = GetProperty(alias);

            if (recurse && Parent != null && prop == null)
                return Parent.GetProperty(alias, true);

            return prop;
        }

        public override IPublishedContent Parent
        {
            get { return _parentNode; }
        }

        public override IEnumerable<IPublishedContent> Children
        {
            get { return _children; }
        }

        public override int TemplateId
        {
            get { return 0; }
        }

        public override int SortOrder
        {
            get { return _sortOrder; }
        }

        public override string UrlName
        {
            get { return null; }
        }

        public override string WriterName
        {
            get { return _containerNode != null ? _containerNode.WriterName : null; }
        }

        public override string CreatorName
        {
            get { return _containerNode != null ? _containerNode.CreatorName : null; }
        }

        public override int WriterId
        {
            get { return _containerNode != null ? _containerNode.WriterId : 0; }
        }

        public override int CreatorId
        {
            get { return _containerNode != null ? _containerNode.CreatorId : 0; }
        }

        public override string Path
        {
            get { return null; }
        }

        public override DateTime CreateDate
        {
            get { return _containerNode != null ? _containerNode.CreateDate : DateTime.MinValue; }
        }

        public override DateTime UpdateDate
        {
            get { return _containerNode != null ? _containerNode.UpdateDate : DateTime.MinValue; }
        }

        public override Guid Version
        {
            get { return _containerNode != null ? _containerNode.Version : Guid.Empty; }
        }

        public override int Level
        {
            get { return _level; }
        }
    }
}
