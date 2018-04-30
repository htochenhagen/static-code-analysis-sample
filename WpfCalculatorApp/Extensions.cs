using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace WpfCalculatorApp
{
    public static class Extensions
    {

        public static string GetErrorMessage(this Exception ex, string message = null, bool stackTrace = true)
        {
            var prefix = string.IsNullOrEmpty(message) ? string.Empty : message + ": ";

            if (ex == null)
                return string.IsNullOrWhiteSpace(message)
                    ? "unknown error, no exception, not even god knows what happened here"
                    : message;
            return prefix + ex.GetErrorMsg(stackTrace);
        }

        private static string GetErrorMsg(this Exception ex, bool includeStackTrace = true)
        {
            if (ex == null) throw new ArgumentNullException(nameof(ex));
            var unwrappedEx = Unwrap(ex);
            var innerMsg = unwrappedEx.InnerException?.Message ?? string.Empty;
            var errorMsg = unwrappedEx.Message;
            if (!string.IsNullOrWhiteSpace(innerMsg))
                errorMsg += " ~ " + innerMsg;
            var stackTrace = unwrappedEx.StackTrace;
            return !includeStackTrace || string.IsNullOrWhiteSpace(stackTrace) ? errorMsg : $"{errorMsg}: {stackTrace}";
        }

        private static Exception Unwrap(Exception ex)
        {
            var targetInvocationException = ex as TargetInvocationException;
            if (targetInvocationException?.InnerException != null)
                return Unwrap(targetInvocationException.InnerException);

            var aggEx = ex as AggregateException;
            if (aggEx != null)
                return AsError<Exception>(aggEx);

            return ex;
        }

        private static Exception AsError<TPreferredException>(AggregateException aggregateException)
        {
            // If aggregateException contains any fatal exceptions, return it directly
            // without tracing it or any inner exceptions.
            if (IsFatal(aggregateException))
                return aggregateException;

            // Collapse possibly nested graph into a flat list.
            // Empty inner exception list is unlikely but possible via public api.
            var innerExceptions = aggregateException.Flatten().InnerExceptions;
            if (innerExceptions.Count == 0)
                return aggregateException;

            // Find the first inner exception, giving precedence to TPreferredException
            Exception favoredException = null;
            foreach (var nextInnerException in innerExceptions)
            {
                // AggregateException may wrap TargetInvocationException, so unwrap those as well
                var unwrappedException = Unwrap(nextInnerException);

                if (unwrappedException is TPreferredException && favoredException == null)
                    favoredException = unwrappedException;
            }

            return favoredException ?? innerExceptions[0];
        }

        private static bool IsFatal(Exception exception)
        {
            // copied from http://referencesource.microsoft.com/#System.ServiceModel.Internals/System/Runtime/Fx.cs,b31712af9bfcc1cb
            while (exception != null)
            {
                if (exception is OutOfMemoryException && !(exception is InsufficientMemoryException) ||
                    exception is ThreadAbortException)
                    return true;

                // These exceptions aren't themselves fatal, but since the CLR uses them to wrap other exceptions,
                // we want to check to see whether they've been used to wrap a fatal exception.  If so, then they
                // count as fatal.
                if (exception is TypeInitializationException ||
                    exception is TargetInvocationException)
                {
                    exception = exception.InnerException;
                }
                else if (exception is AggregateException)
                {
                    // AggregateExceptions have a collection of inner exceptions, which may themselves be other
                    // wrapping exceptions (including nested AggregateExceptions).  Recursively walk this
                    // hierarchy.  The (singular) InnerException is included in the collection.
                    var innerExceptions = ((AggregateException)exception).InnerExceptions;
                    if (innerExceptions.Any(IsFatal))
                        return true;

                    break;
                }
                else
                {
                    break;
                }
            }

            return false;
        }

        public static void BringToFront(this Window window)
        {
            // cf.: http://stackoverflow.com/questions/257587/bring-a-window-to-the-front-in-wpf#4831839
            if (!window.IsVisible)
                window.Show();
            if (window.WindowState == WindowState.Minimized)
                window.WindowState = WindowState.Normal;
            window.Activate();
            window.Topmost = true;
            window.Topmost = false;
        }


    }
}
