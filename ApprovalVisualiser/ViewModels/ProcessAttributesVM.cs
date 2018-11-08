using ApprovalVisualiser.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalVisualiser.ViewModels
{
    public class ProcessAttributesVM : ObservableCollection<AvailableProperty>
    {

      
        public void InitializeData(string data)
        {

            //var template = System.IO.File.ReadAllText(richText);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(data);

            var root = (CompilationUnitSyntax)tree.GetRoot();

            var firstMember = root.Members[0];

            var helloWorldDeclaration = (NamespaceDeclarationSyntax)firstMember;

            ClassDeclarationSyntax programDeclaration = (ClassDeclarationSyntax)helloWorldDeclaration.Members[0];

            var className = programDeclaration.TryGetInferredMemberName();


           // _AvailableProperties = new ObservableCollection<AvailableProperty>();

            foreach (var m in programDeclaration.Members)
            {
                if (m is PropertyDeclarationSyntax)
                {
                    AddProperty(m);
                    Console.WriteLine($"{(m as PropertyDeclarationSyntax).Identifier.Text} {(m as PropertyDeclarationSyntax).Type}");
                }


                //m.ide
            }
        }


        private void AddProperty(MemberDeclarationSyntax m)
        {
            this.Add(new AvailableProperty
            {
                PropertyName = (m as PropertyDeclarationSyntax).Identifier.Text,
                PopertyType = (m as PropertyDeclarationSyntax).Type.ToString(),
                // TP = ((m as PropertyDeclarationSyntax).Type.GetType())
            });
        }
    }
}
