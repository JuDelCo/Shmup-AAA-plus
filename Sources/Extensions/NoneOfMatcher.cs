﻿using System.Collections.Generic;

namespace Entitas
{
	public class NoneOfMatcher : AbstractMatcher
	{
		public NoneOfMatcher(int[] indices) : base(indices) {}

		public override bool Matches(Entity entity)
		{
			for (int i = 0, indicesLength = indices.Length; i < indicesLength; i++)
			{
				if (entity.HasComponent(indices[i]))
				{
					return false;
				}
			}

			return true;
		}
	}

	public partial class Matcher
	{
		public static NoneOfMatcher NoneOf(params int[] indices)
		{
			return new NoneOfMatcher(indices);
		}

		public static NoneOfMatcher NoneOf(params NoneOfMatcher[] matchers)
		{
			var indices = new List<int>();
			
			for (int i = 0, matchersLength = matchers.Length; i < matchersLength; i++)
			{
				indices.AddRange(matchers[i].indices);
			}

			return new NoneOfMatcher(indices.ToArray());
		}
	}
}
