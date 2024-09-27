using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.NET8.Fundamentals;
using Assert = NUnit.Framework.Assert;

namespace TestNinja.NET8.UnitTests
{
	[TestFixture]
	public class StackTests
	{
		/* stack:
		 * Count property - get the count
		 * Push method
		 *		throw argument null exception if a null argument is passed
		 *		if a non-null argument is passed, added to the stack and the count should be 1
		 * Pop method
		 *		throw invalid operation exception if no items are in the list
		 *		if there are items in the list, calling this method removes the item with the highest index in the list
		 *			this also reduces the count by 1
		 *		if there are two items in the list, calling Pop three times will cause the invalid operation exception to be thrown
		 * Peek method
		 *		if no items are in the list an invalid operation exception will be thrown
		 *		if items are in the list, calling Peek will return the item in the list with the highest index
		 */

		private Fundamentals.Stack<string> _stack;

		[SetUp]
		public void SetUp()
		{
			_stack = new Fundamentals.Stack<string>();
		}


		[Test]
		public void Push_NullArgument_ThrowsArgumentNullExpression()
		{
			// arrange

			// act/assert
			Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
		}

		[Test]
		public void Push_ValidArgument_CountIsOne()
		{
			// arrange


			// act
			_stack.Push("abc");

			// assert
			Assert.That(_stack.Count, Is.EqualTo(1));
		}

		[Test]
		public void Pop_NoItemsInList_ThrowsInvalidOperationException()
		{
			// arrange


			// act


			// assert
			Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
		}

		[Test]
		public void Pop_TwoItemsInList_CountIsOne()
		{
			// arrange
			_stack.Push("abc");
			_stack.Push("def");

			// act
			var result = _stack.Pop();

			// assert
			Assert.That(result, Is.EqualTo("def"));
			Assert.That(_stack.Count, Is.EqualTo(1));
		}

		[Test]
		public void Pop_TwoItemsInListPopCalled3Times_ThrowsInvalidOperationException()
		{
			// arrange
			_stack.Push("abc");
			_stack.Push("def");

			// act
			_stack.Pop();
			_stack.Pop();

			// assert
			Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
		}

		[Test]
		public void Peek_NoItemsInList_ThrowsInvalidOperationException()
		{
			// arrange


			// act


			// assert
			Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
		}

		[Test]
		public void Peek_TwoItemsInList_ReturnsLastItemAndCountIsTwo()
		{
			// arrange
			_stack.Push("abc");
			_stack.Push("def");

			// act
			var result = _stack.Peek();

			// assert
			Assert.That(result, Is.EqualTo("def"));
		}
	}
}
