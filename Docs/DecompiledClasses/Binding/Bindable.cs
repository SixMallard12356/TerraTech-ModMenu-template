using System;

namespace Binding;

public class Bindable<T>
{
	public Event<T> Changed;

	private T m_Value;

	public T Value
	{
		get
		{
			return m_Value;
		}
		set
		{
			m_Value = value;
			Changed.Send(m_Value);
		}
	}

	public Bindable()
	{
		m_Value = default(T);
	}

	public Bindable(T value)
	{
		m_Value = value;
	}

	public void Bind(Action<T> listener)
	{
		if (listener == null)
		{
			throw new ArgumentException("Bindable - lister can not be null");
		}
		Changed.Unsubscribe(listener);
		Changed.Subscribe(listener);
		listener(Value);
	}

	public void Unbind(Action<T> listener)
	{
		if (listener == null)
		{
			throw new ArgumentException("Bindable - lister can not be null");
		}
		Changed.Unsubscribe(listener);
	}

	public void Bind(Bindable<T> bindable)
	{
		if (bindable == null)
		{
			throw new ArgumentException("Bindable - can not be null");
		}
		Changed.Unsubscribe(bindable.SetValue);
		Changed.Subscribe(bindable.SetValue);
		bindable.SetValue(Value);
	}

	public void Unbind(Bindable<T> bindable)
	{
		Changed.Unsubscribe(bindable.SetValue);
	}

	private void SetValue(T value)
	{
		Value = value;
	}
}
