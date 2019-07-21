using NBench;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp;

namespace Web.Api.Tests
{
    [SetUpFixture]
    public class TestSetup
    {
         
        public void InitializeOneTimeData()
        {
            AutoMapperConfig.Initialize();
        }

        
        public void TearDown()
        {
            AutoMapper.Mapper.Reset();
        }
    }
}
