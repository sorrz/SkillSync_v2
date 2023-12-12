export function toHaveText(element: HTMLElement, expectedText: string) {
    if (element.textContent !== expectedText) {
      throw new Error(`Element does not contain: "${expectedText}"`);
    }
  }
  
  export function toHaveAttribute(element: HTMLElement, attributeName: string, expectedValue: string) {
    const actualValue = element.getAttribute(attributeName);
    if (actualValue !== expectedValue) {
      throw new Error(`Element does not have attribute "${attributeName}" with value "${expectedValue}"`);
    }
  }
  