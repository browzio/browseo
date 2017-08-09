#region ***** BEGIN LICENSE BLOCK *****
/* Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is Skybound Software code.
 *
 * The Initial Developer of the Original Code is Skybound Software.
 * Portions created by the Initial Developer are Copyright (C) 2008-2009
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 */
#endregion END LICENSE BLOCK

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Runtime.CompilerServices;

namespace Gecko.Interfaces
{
	/// <summary>
	/// The signature of the writer function passed to ReadSegments. This
	/// is the &quot;consumer&quot; of data that gets read from the stream's buffer.
	/// </summary>
	/// <param name="aInStream">The stream being read.</param>
	/// <param name="aClosure">The opaque parameter passed to ReadSegments</param>
	/// <param name="aFromSegment">
	/// The pointer to memory owned by the input stream.
	/// This is where the writer function should start consuming data.
	/// </param>
	/// <param name="aToOffset">The amount of data already consumed by
	/// this writer during this ReadSegments call. This is also the sum
	/// of the aWriteCount returns from this writer over the previous
	/// invocations of the writer by this ReadSegments call.
	/// </param>
	/// <param name="aCount">Number of bytes available to be read starting at aFromSegment.</param>
	/// <param name="aWriteCount">Number of bytes read by this writer function call.</param>
	/// <returns>
	/// NS_OK and (*aWriteCount &gt; 0) if consumed some data.
	/// &lt;any-error&gt; if not interested in consuming any data.
	/// </returns>
	/// <remarks>
	/// Errors are never passed to the caller of ReadSegments.
	/// Returning NS_OK and (*aWriteCount = 0) has undefined behavior.
	/// </remarks>
	public delegate int nsWriteSegmentFun(nsIInputStream aInStream, IntPtr aClosure, IntPtr aFromSegment, int aToOffset, int aCount, out int aWriteCount);

	[ComImport]
	[Guid("769693d4-b009-4fe2-af18-7dc8df7496df")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsPIDOMWindowOuter { }


}
