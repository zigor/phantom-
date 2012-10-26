// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestBasicOperations.cs" company="">
//   
// </copyright>
// <summary>
//   The unit test 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Phantom.Tests.Sample
{
  using System.Collections.ObjectModel;

  using FluentAssertions;

  using Microsoft.VisualStudio.TestTools.UnitTesting;

  using Phantom.TestKit.Data;
  using Phantom.TestKit.Sites;

  using Sitecore;
  using Sitecore.Collections;
  using Sitecore.Data;
  using Sitecore.Security.AccessControl;
  using Sitecore.Security.Accounts;
  using Sitecore.Security.Authentication;
  using Sitecore.Sites;

  /// <summary>
  /// The unit test 1.
  /// </summary>
  [TestClass]
  public class TestBasicOperations
  {
    #region Public Methods and Operators

    /// <summary>
    /// Should apply security check.
    /// </summary>
    [TestMethod]
    public void ShouldApplySecurityCheck()
    {
      // Arrange
      using (var tree = new TTree())
      {
        User user = AuthenticationManager.BuildVirtualUser("User", true);
        user.RuntimeSettings.IsAdministrator = false;
        
        var rules = new AccessRuleCollection
        {
           AccessRule.Create(user, AccessRight.ItemRead, PropagationType.Any, AccessPermission.Deny) 
        };

        const string Path = "/sitecore/content/home";
        var item = tree.Database.GetItem(Path);
        item.Security.SetAccessRules(rules);

        // Act & Assert
        using (var switcher = new UserSwitcher(user))
        {
          tree.Database.GetItem(Path).Should().BeNull();
        }
      }
    }

    /// <summary>
    /// Should create and switch to simple site context.
    /// </summary>
    [TestMethod]
    public void ShouldCreateAndSwitchToSimpleSiteContext()
    {
      // Arrange
      var website = new TSiteContext("website");

      // Act
      using (new SiteContextSwitcher(website))
      {
        // Assert
        Context.Site.Name.Should().Be("website");
      }
    }

    /// <summary>
    /// Should create and switch to site context with custom propeties.
    /// </summary>
    [TestMethod]
    public void ShouldCreateAndSwitchToSiteContextWithCustomPropeties()
    {
      using (new TTree())
      {
        // Arrange
        var website = new TSiteContext( new StringDictionary { { "name", "webshop" }, { "content", "master" }, { "settingsItem", "/settings" } });

        // Act
        using (new SiteContextSwitcher(website))
        {
          var site = Context.Site;

          // Assert
          site.Name.Should().Be("webshop");
          site.ContentDatabase.Name.Should().Be("master");
          site.Properties["settingsItem"].Should().Be("/settings");
        }
      }
    }

    /// <summary>
    /// Should create few items on the same template.
    /// </summary>
    [TestMethod]
    public void ShouldCreateFewItemsOnTheSameTemplateAndReadFields()
    {
      // Arrange
      ID templateId = ID.NewID;
      using (
        var tree = new TTree
        {
          new TTemplate("Product", templateId) { new TSection("Data") { new Collection<string> { "Title", "Price" } } }, 
          new TItem("My Camera", new TemplateID(templateId)) { { "Title", "My Camera" }, { "Price", "$1000" } }, 
          new TItem("My Laptop", new TemplateID(templateId)) { { "Title", "My Laptop" }, { "Price", "$1200" } }
        })
      {
        // Act
        var camera = tree.Database.GetItem("/sitecore/content/home/my camera");
        var laptop = tree.Database.GetItem("/sitecore/content/home/my laptop");

        // Assert
        camera.TemplateID.Guid.Should().Be(templateId.Guid);
        camera.Fields["Title"].Value.Should().Be("My Camera");
        camera.Fields["Price"].Value.Should().Be("$1000");

        laptop.TemplateID.Guid.Should().Be(templateId.Guid);
        laptop.Fields["Title"].Value.Should().Be("My Laptop");
        laptop.Fields["Price"].Value.Should().Be("$1200");
      }
    }

    /// <summary>
    /// Should create item hierarchy and read root and children.
    /// </summary>
    [TestMethod]
    public void ShouldCreateItemHierarchyAndReadRootAndChildren()
    {
      // Arrange
      using (
        var tree = new TTree
        {
          new TItem("Products")
          {
             new TItem("Camera") { { "Price", "$1000" } }, new TItem("Laptop") { { "Price", "$2000" } } 
          }
        })
      {
        // Act
        var products = tree.Database.GetItem("/sitecore/content/home/products");

        // Assert
        products.Children["Camera"]["Price"].Should().Be("$1000");
        products.Children["Laptop"]["Price"].Should().Be("$2000");
      }
    }

    /// <summary>
    /// Should create test tree and read default database.
    /// </summary>
    [TestMethod]
    public void ShouldCreateTestTreeAndReadDefaultDatabase()
    {
      // Arrange
      using (var tree = new TTree("master"))
      {
        // Act
        Database database = tree.Database;

        // Assert
        database.Name.Should().Be("master");
      }
    }

    /// <summary>
    /// Should create test tree and read default home item.
    /// </summary>
    [TestMethod]
    public void ShouldCreateTestTreeAndReadDefaultHomeItem()
    {
      // Arrange
      using (var tree = new TTree())
      {
        // Act
        var home = tree.Database.GetItem("/sitecore/content/home");

        // Assert
        home.Name.Should().Be("home");
      }
    }

    /// <summary>
    /// Should create test tree and read test item.
    /// </summary>
    [TestMethod]
    public void ShouldCreateTestTreeAndReadTestItem()
    {
      // Arrange
      using (var tree = new TTree { new TItem("Sample") { { "Title", "Sample item" } } })
      {
        // Act
        var result = tree.Database.GetItem("/sitecore/content/home/sample");

        // Assert
        result.Should().NotBeNull();
        result["Title"].Should().Be("Sample item");
      }
    }

    /// <summary>
    /// Should create test tree with custom database.
    /// </summary>
    [TestMethod]
    public void ShouldCreateTestTreeWithCustomDatabase()
    {
      // Arrange
      using (var tree = new TTree("core"))
      {
        // Act
        Database database = tree.Database;

        // Assert
        database.Name.Should().Be("core");
      }
    }

    #endregion
  }
}