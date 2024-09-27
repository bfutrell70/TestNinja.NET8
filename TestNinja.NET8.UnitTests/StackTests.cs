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
		 *		with empty stack should be 0
		 *		when object is added to stack count should be 1
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
		 *		   and the count should be the same as it was before the Peek call
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
			// act
			_stack.Push("abc");

			// assert
			Assert.That(_stack.Count, Is.EqualTo(1));
		}

		[Test]
		public void Count_EmptyStack_IsZero()
		{
			// assert
			Assert.That(_stack.Count, Is.EqualTo(0));
		}

		[Test]
		public void Pop_NoItemsInList_ThrowsInvalidOperationException()
		{
			// assert
			Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
		}

		[Test]
		public void Pop_StackWithTwoObjects_ReturnObjectFromTheTop()
		{
			// Mosh separated the tests for returning the last object
			// and verifying the count - each test should be for a
			// single item

			// arrange
			_stack.Push("a");
			_stack.Push("b");
			_stack.Push("c");

			// act
			var result = _stack.Pop();

			// assert
			Assert.That(result, Is.EqualTo("c"));
		}

		[Test]
		public void Pop_StackWithTwoObjects_RemoveObjectFromTheTop()
		{
			// Mosh separated the tests for returning the last object
			// and verifying the count so that each test is verifying
			// a single item

			// arrange
			_stack.Push("a");
			_stack.Push("b");
			_stack.Push("c");

			// act
			_stack.Pop();

			// assert
			Assert.That(_stack.Count, Is.EqualTo(2));
		}

		// my original test - tests two things
		// [Test]
		// public void Pop_TwoItemsInList_CountIsOne()
		// {
		// 	// arrange
		// 	_stack.Push("abc");
		// 	_stack.Push("def");

		// 	// act
		// 	var result = _stack.Pop();

		// 	// assert
		// 	Assert.That(result, Is.EqualTo("def"));
		// 	Assert.That(_stack.Count, Is.EqualTo(1));
		// }

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
			// assert
			Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
		}

		[Test]
		public void Peek_TwoItemsInList_DoesNotRemoveObjectOnTopOfTheStack()
		{
			// Mosh separates object check and count check since each unit test
			// should test for one thing

			// arrange
			_stack.Push("a");
			_stack.Push("b");
			_stack.Push("c");

			// act
			_stack.Peek();

			// assert
			Assert.That(_stack.Count, Is.EqualTo(3));
		}

		[Test]
		public void Peek_TwoItemsInList_ReturnsObjectOnTopOfTheStack()
		{
			// Mosh separates object check and count check since each unit test
			// should test for one thing
			
			// arrange
			_stack.Push("a");
			_stack.Push("b");
			_stack.Push("c");

			// act
			var result = _stack.Peek();

			// assert
			Assert.That(result, Is.EqualTo("c"));
		}

		// My original codde
		// [Test]
		// public void Peek_TwoItemsInList_ReturnsLastItemAndCountIsTwo()
		// {
		// 	// arrange
		// 	_stack.Push("abc");
		// 	_stack.Push("def");

		// 	// act
		// 	var result = _stack.Peek();

		// 	// assert
		// 	Assert.That(result, Is.EqualTo("def"));
		// 	Assert.That(_stack.Count, Is.EqualTo(2));
		// }
	}
}
