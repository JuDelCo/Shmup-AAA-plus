using System.Collections.Generic;

namespace Entitas
{
	public class AnyOfMatcher : AbstractMatcher
	{
		public AnyOfMatcher(int[] indices) : base(indices) {}

		public override bool Matches(Entity entity)
		{
			return entity.HasAnyComponent(indices);
		}
	}

	public partial class Matcher
	{
		public static AnyOfMatcher AnyOf(params int[] indices)
		{
			return new AnyOfMatcher(indices);
		}

		public static AnyOfMatcher AnyOf(params AnyOfMatcher[] matchers)
		{
			var indices = new List<int>();
			
			for (int i = 0, matchersLength = matchers.Length; i < matchersLength; i++)
			{
				indices.AddRange(matchers[i].indices);
			}

			return new AnyOfMatcher(indices.ToArray());
		}
	}
}
