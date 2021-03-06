﻿using NUnit.Framework;
using SpecUnit;
using StructureMap;

namespace UoW.Specs
{

	public abstract class RepositoryExecutionContext : BaseMockUoWContext {}

	[TestFixture]
	[Concern("Transaction Execution")]
	public class When_executing_a_repository_method : RepositoryExecutionContext
	{

		public class MockFooRepo : IFooRepo
		{
			public void Something()
			{
			}
		}

		private interface IFooRepo
		{
			void Something();
		}

		private IFooRepo fooRepo;

		protected override void Context()
		{
			fooRepo = new MockFooRepo();

			ObjectFactory.Initialize(
				factory => factory
					.ForRequestedType<IFooRepo>()
					.TheDefaultIsConcreteType<MockFooRepo>());			

			UnitOfWork.Start(() => 
				Repository<IFooRepo>.Do.Something()
			);
		}

		[Test]
		[Observation]
		public void Should_retrieve_an_instance_of_the_specified_repository()
		{
			fooRepo.ShouldNotBeNull();
		}

	}

}